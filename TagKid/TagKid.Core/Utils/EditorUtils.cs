using System;
using System.Collections.Generic;
using System.Text;

namespace TagKid.Core.Utils
{
    public class EditorUtils
    {
        public static string ToHtml(string contentCode)
        {
            var html = new StringBuilder();
            var linkText = String.Empty;

            var replace = new Dictionary<char, string>
            {
                {'<', "&lt;"},
                {'>', "&gt;"},
                {'&', "&amp;"},
                {'\n', "<br/>"},
                {'\t', "&nbsp;&nbsp;&nbsp;&nbsp;"},
                {'\r', ""}
            };

            var tagBuilders = new Dictionary<char, TagBuilder>
            {
                {'/', new TagBuilder("i")},
                {'*', new TagBuilder("b")},
                {'_', new TagBuilder("u")},
                {'-', new TagBuilder("s")},
                {'`', new TagBuilder("pre")}
            };

            var inPre = false;
            var inLink = false;

            for (var i = 0; i < contentCode.Length; i++)
            {
                var c = contentCode[i];

                if (c == '[' && !inPre)
                {
                    if (inLink)
                    {
                        linkText += c;
                    }
                    else
                    {
                        if (i < contentCode.Length - 1)
                        {
                            var nextChar = contentCode[i + 1];
                            if (nextChar == c)
                            {
                                html.Append(c);
                                i++;
                                continue;
                            }
                        }
                        inLink = true;
                        linkText = String.Empty;
                    }
                }
                else if (c == ']' && !inPre)
                {
                    if (inLink)
                    {
                        inLink = false;
                        var index = linkText.LastIndexOf('|');

                        if (index < 1)
                        {
                            html.Append("<img class='img-responsive' src='")
                                .Append(linkText.Trim())
                                .Append("' />");
                        }
                        else
                        {
                            var href = linkText.Substring(index + 1);

                            if (href.IndexOf("http://") != 0 &&
                                href.IndexOf("https://") != 0)
                            {
                                href = "http://" + href.Trim();
                            }

                            html.Append("<a href='")
                                .Append(href)
                                .Append("' target='_blank'>")
                                .Append(linkText.Substring(0, index).Trim())
                                .Append("</a>");
                        }
                    }
                    else
                    {
                        html.Append(c);
                    }
                }
                else if (inLink)
                {
                    if (replace.ContainsKey(c))
                        linkText += replace[c];
                    else
                        linkText += c;
                }
                else if (replace.ContainsKey(c))
                {
                    html.Append(replace[c]);
                }
                else if (tagBuilders.ContainsKey(c))
                {
                    if (c == '`')
                        inPre = !inPre;

                    if (inPre && c != '`')
                    {
                        html.Append(c);
                        continue;
                    }

                    if (i < contentCode.Length - 1)
                    {
                        var nextChar = contentCode[i + 1];
                        if (nextChar == c)
                        {
                            html.Append(c);
                            i++;
                            continue;
                        }
                    }

                    html.Append(tagBuilders[c].Build());
                }
                else
                {
                    html.Append(c);
                }
            }

            return html.ToString();
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