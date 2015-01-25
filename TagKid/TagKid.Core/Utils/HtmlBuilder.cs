using System;
using System.Collections.Generic;
using System.Text;

namespace TagKid.Core.Utils
{
    public class HtmlBuilder
    {
        private static readonly Dictionary<char, string> Replace = new Dictionary<char, string>
        {
            { '<', "&lt;" },
            { '>', "&gt;" },
            { '&', "&amp;" },
            { '\n', "<br/>" },
            { '\t', "&nbsp;&nbsp;&nbsp;&nbsp;" },
            { '\r', "" }
        };

        private static readonly Dictionary<char, TagBuilder> TagBuilders = new Dictionary<char, TagBuilder>
        {
            { '/', new TagBuilder("i") },
            { '*', new TagBuilder("b") },
            { '_', new TagBuilder("u") },
            { '-', new TagBuilder("s") },
            { '`', new TagBuilder("pre") }
        };

        private readonly StringBuilder _html;
        private readonly StringBuilder _linkText;

        private bool _inPre;
        private bool _inLink;
        private bool _isYoutubeLink;
        private bool _isSoundCloudLink;
        private bool _isVimeoLink;

        private HtmlBuilder()
        {
            _html = new StringBuilder();
            _linkText = new StringBuilder();
        }

        private string BuildHtml(string text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '[' && !_inPre)
                {
                    if (_inLink)
                    {
                        _linkText.Append(c);
                    }
                    else
                    {
                        if (i < text.Length - 1)
                        {
                            var nextChar = text[i + 1];
                            if (nextChar == c)
                            {
                                _html.Append(c);
                                i++;
                                continue;
                            }
                        }
                        _inLink = true;
                        _linkText.Clear();
                    }
                }
                else if (c == ']' && !_inPre)
                {
                    if (_inLink)
                    {
                        _inLink = false;

                        if (_isYoutubeLink)
                        {
                            BuildYoutubeLink();
                        }
                        else if (_isSoundCloudLink)
                        {
                            BuildSoundCloudLink();
                        }
                        else if (_isVimeoLink)
                        {
                            BuildVimeoLink();
                        }
                        else
                        {
                            var index = _linkText.LastIndexOf('|');

                            if (index < 1)
                            {
                                BuildImgLink();
                            }
                            else
                            {
                                BuildLink(index);
                            }
                        }
                    }
                    else
                    {
                        _html.Append(c);
                    }
                }
                else if (_inLink)
                {
                    if (Replace.ContainsKey(c))
                    {
                        _linkText.Append(Replace[c]);
                    }
                    else
                    {
                        _linkText.Append(c);
                    }

                    if (_linkText.BufferEquals("youtube:"))
                    {
                        _linkText.Clear();
                        _isYoutubeLink = true;
                    }
                    else if (_linkText.BufferEquals("soundcloud:"))
                    {
                        _linkText.Clear();
                        _isSoundCloudLink = true;
                    }
                    else if (_linkText.BufferEquals("vimeo:"))
                    {
                        _linkText.Clear();
                        _isVimeoLink = true;
                    }
                }
                else if (Replace.ContainsKey(c))
                {
                    _html.Append(Replace[c]);
                }
                else if (TagBuilders.ContainsKey(c))
                {
                    if (c == '`')
                    {
                        _inPre = !_inPre;
                    }

                    if (_inPre && c != '`')
                    {
                        _html.Append(c);
                        continue;
                    }
                    if (i < text.Length - 1)
                    {
                        var nextChar = text[i + 1];
                        if (nextChar == c)
                        {
                            _html.Append(c);
                            i++;
                            continue;
                        }
                    }

                    _html.Append(TagBuilders[c].Build());
                }
                else
                {
                    _html.Append(c);
                }
            }

            return _html.ToString();
        }

        private void BuildYoutubeLink()
        {
            _isYoutubeLink = false;

            var linkStr = _linkText.ToString().Trim();

            _html.Append("<div class='flex-video widescreen'>")
                .Append("<iframe src='//www.youtube.com/embed/")
                .Append(linkStr)
                .Append("?html5=1' frameborder='0' allowfullscreen></iframe>")
                .Append("</div>");

            //var imgUrl = "http://img.youtube.com/vi/" + linkStr + "/hqdefault.jpg'";

            //_html.Append("<div class='youtube-preview' style='")
            //    .Append("background-image: url(")
            //    .Append(imgUrl)
            //    .Append(");'>")
            //    .Append("<a class='play-button' href='#")
            //    .Append(linkStr)
            //    .Append("'><i class'fa fa-4x fa-youtube-play'></i></a>")
            //    .Append("<img class='img-responsive' src='")
            //    .Append(imgUrl)
            //    .Append("' style='visibility:hidden'/></div>");
        }

        private void BuildSoundCloudLink()
        {
            _isSoundCloudLink = false;

            var linkStr = _linkText.ToString().Trim();

            _html
                .Append("<iframe width='100%' height='166' scrolling='no' frameborder='no' ")
                .Append("src='https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/")
                .Append(linkStr)
                .Append(
                    "&amp;color=ff9900&amp;auto_play=false&amp;hide_related=true&amp;show_comments=false&amp;show_user=true&amp;show_reposts=false'>")
                .Append("</iframe>");
        }

        private void BuildVimeoLink()
        {
            _isVimeoLink = false;

            var linkStr = _linkText.ToString().Trim();

            _html.Append("<div class='flex-video vimeo widescreen'>")
                .Append("<iframe src='//player.vimeo.com/video/")
                .Append(linkStr)
                .Append(
                    "?title=0&amp;byline=0&amp;portrait=0&amp;badge=0&amp;color=ffffff' width='500' height='213' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>")
                .Append("</div>");
        }

        private void BuildImgLink()
        {
            var linkStr = _linkText.ToString().Trim();

            _html.Append("<img class='img-responsive' src='")
                .Append(linkStr)
                .Append("' />");
        }

        private void BuildLink(int index)
        {
            var linkStr = _linkText.ToString().Trim();
            var href = linkStr.Substring(index + 1);

            if (href.IndexOf("http://", StringComparison.Ordinal) != 0 &&
                href.IndexOf("https://", StringComparison.Ordinal) != 0)
            {
                href = "http://" + href.Trim();
            }

            _html.Append("<a href='")
                .Append(href)
                .Append("' target='_blank'>")
                .Append(linkStr.Substring(0, index).Trim())
                .Append("</a>");
        }

        public static string ToHtml(string editorContent)
        {
            var builder = new HtmlBuilder();
            return builder.BuildHtml(editorContent);
        }

        private class TagBuilder
        {
            private readonly string _tag;
            private int _count;

            public TagBuilder(string htmlTag)
            {
                _count = 0;
                _tag = htmlTag;
            }

            public string Build()
            {
                if (_count == 0)
                {
                    _count = 1;
                    return "<" + _tag + ">";
                }
                _count = 0;
                return "</" + _tag + ">";
            }
        }
    }
}