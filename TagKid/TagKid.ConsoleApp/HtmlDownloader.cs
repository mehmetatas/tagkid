using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace TagKid.ConsoleApp
{
    public class HtmlDownloader
    {
        private readonly string _referer;
        private readonly string _userAgent;

        public Encoding Encoding { get; set; }
        public WebHeaderCollection Headers { get; set; }
        public Uri Url { get; set; }

        public HtmlDownloader(string url)
            : this(url, null, null)
        {
        }

        public HtmlDownloader(string url, string referer, string userAgent)
        {
            Encoding = Encoding.UTF8;
            Url = new Uri(url); // verify the uri
            _userAgent = userAgent;
            _referer = referer;
        }

        public void GetHtml()
        {
            var request = (HttpWebRequest)WebRequest.Create(Url);
            if (!string.IsNullOrEmpty(_referer))
                request.Referer = _referer;
            if (!string.IsNullOrEmpty(_userAgent))
                request.UserAgent = _userAgent;

            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                Headers = response.Headers;
                Url = response.ResponseUri;
                ProcessContent(response);
            }
        }

        public string HtmlContent { get; private set; }

        public bool IsImage { get; private set; }
        public bool IsHtml { get; private set; }

        private void ProcessContent(HttpWebResponse response)
        {
            SetEncodingFromHeader(response);

            var s = response.GetResponseStream();
            if (response.ContentEncoding.ToLower().Contains("gzip"))
                s = new GZipStream(s, CompressionMode.Decompress);
            else if (response.ContentEncoding.ToLower().Contains("deflate"))
                s = new DeflateStream(s, CompressionMode.Decompress);

            var memStream = new MemoryStream();
            int bytesRead;
            var buffer = new byte[1024];
            for (bytesRead = s.Read(buffer, 0, buffer.Length); bytesRead > 0; bytesRead = s.Read(buffer, 0, buffer.Length))
            {
                memStream.Write(buffer, 0, bytesRead);
            }
            s.Close();

            if (IsHtml)
            {
                memStream.Position = 0;
                using (var r = new StreamReader(memStream, Encoding))
                {
                    HtmlContent = r.ReadToEnd().Trim();
                    HtmlContent = CheckMetaCharSetAndReEncode(memStream, HtmlContent);
                }
            }
        }

        private void SetEncodingFromHeader(HttpWebResponse response)
        {
            if (response.ContentType.ToLower(CultureInfo.InvariantCulture).StartsWith("image/"))
            {
                IsImage = true;
                return;
            }

            IsHtml = true;

            string charset = null;
            if (string.IsNullOrEmpty(response.CharacterSet))
            {
                var m = Regex.Match(response.ContentType, @";\s*charset\s*=\s*(?<charset>.*)", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    charset = m.Groups["charset"].Value.Trim(new[] { '\'', '"' });
                }
            }
            else
            {
                charset = response.CharacterSet;
            }
            if (!string.IsNullOrEmpty(charset))
            {
                try
                {
                    Encoding = Encoding.GetEncoding(charset);
                }
                catch (ArgumentException)
                {
                }
            }
        }

        private string CheckMetaCharSetAndReEncode(Stream memStream, string html)
        {
            var m = new Regex(@"<meta\s+.*?charset\s*=\s*(?<charset>[A-Za-z0-9_-]+)", RegexOptions.Singleline | RegexOptions.IgnoreCase).Match(html);
            if (m.Success)
            {
                var charset = m.Groups["charset"].Value.ToLower();
                if ((charset == "unicode") || (charset == "utf-16"))
                {
                    charset = "utf-8";
                }

                try
                {
                    var metaEncoding = Encoding.GetEncoding(charset);
                    if (Encoding != metaEncoding)
                    {
                        memStream.Position = 0L;
                        var recodeReader = new StreamReader(memStream, metaEncoding);
                        html = recodeReader.ReadToEnd().Trim();
                        recodeReader.Close();
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            return html;
        }
    }
}