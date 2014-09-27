tgEditor = {
    $input: null,
    $preview: null,
    init: function (editorSelector, previewDivSelector) {
        tgEditor.$input = $(editorSelector);
        tgEditor.$preview = $(previewDivSelector);

        $(editorSelector).autoGrow();

        var timeout;
        $(editorSelector).on('keyup change', function () {
            if (timeout)
                clearTimeout(timeout);

            timeout = setTimeout(tgEditor.format, 500);
        });

        tgEditor.format();
    },
    format: function () {
        var html = tgEditor.toHtml(tgEditor.$input.val());
        tgEditor.$preview.html(html);
    },
    toHtml: function (text) {
        var html = '';
        var linkText = '';

        var replace = {
            '<': '&lt;',
            '>': '&gt;',
            '&': '&amp;',
            '\n': '<br/>',
            '\t': '&nbsp;&nbsp;&nbsp;&nbsp;',
            '\r': ''
        };

        var tagBuilders = {
            '/': new TagBuilder('i'),
            '*': new TagBuilder('b'),
            '_': new TagBuilder('u'),
            '-': new TagBuilder('s'),
            '`': new TagBuilder('pre')
        };

        var inPre = false;
        var inLink = false;

        for (var i = 0; i < text.length; i++) {
            var c = text.charAt(i);

            if (c == '[' && !inPre) {
                if (inLink) {
                    linkText += c;
                }
                else {
                    if (i < text.length - 1) {
                        var nextChar = text.charAt(i + 1);
                        if (nextChar == c) {
                            html += c;
                            i++;
                            continue;
                        }
                    }
                    inLink = true;
                    linkText = '';
                }
            }
            else if (c == ']' && !inPre) {
                if (inLink) {
                    inLink = false;
                    var index = linkText.lastIndexOf('|');

                    if (index < 1) {
                        html += '<img class="img-responsive" src="'
                               + linkText.trim()
                               + '" />';
                    } else {
                        var href = linkText.substr(index + 1);

                        if (href.indexOf('http://') != 0 &&
                            href.indexOf('https://') != 0) {
                            href = 'http://' + href.trim();
                        }

                        html += '<a href="'
                            + href
                            + '" target="_blank">'
                            + linkText.substr(0, index).trim()
                            + '</a>';
                    }
                } else {
                    html += c;
                }
            }
            else if (inLink) {
                if (replace[c])
                    linkText += replace[c];
                else
                    linkText += c;
            }
            else if (replace[c]) {
                html += replace[c];
            }
            else if (tagBuilders[c]) {
                if (c == '`')
                    inPre = !inPre;

                if (inPre && c != '`') {
                    html += c;
                    continue;
                } else if (i < text.length - 1) {
                    var nextChar = text.charAt(i + 1);
                    if (nextChar == c) {
                        html += c;
                        i++;
                        continue;
                    }
                }

                html += tagBuilders[c].build();

            } else {
                html += c;
            }
        }

        return html;
    }
};

function TagBuilder(htmlTag) {
    var count = 0;
    var tag = htmlTag;

    this.build = function () {
        if (count == 0) {
            count = 1;
            return '<' + tag + '>';
        } else {
            count = 0;
            return '</' + tag + '>';
        }
    }
}