var tkEditor = {
    create: function (input, preview, title) {
        return new TagKidEditor(input, preview, title);
    }
};

function TagKidEditor(input, preview, title) {
    var $input = $(input);
    var $title = $(title);
    var $preview = $(preview);

    $preview.css('white-space', 'pre-wrap');

    this.toggle = function () {
        sync();

        $input.toggle();
        $title.toggle();
        $preview.toggle();
    }
    
    var sync = function () {
        var htmlBuilder = new HtmlBuilder();
        var html = htmlBuilder.build($input.val());

        if ($title.val().length > 0) {
            html = '<h4>' + $title.val() + '</h4>' + html;
        }

        $preview.html(html);
    };
}

function HtmlBuilder() {
    var html = '';
    var linkText = '';

    var inPre = false;
    var inLink = false;
    var isYoutubeLink = false;
    var isSoundCloudLink = false;
    var isVimeoLink = false;

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

    var buildYoutubeLink = function() {
        isYoutubeLink = false;

        //html += '<div class="flex-video widescreen">'
        //    + '<iframe width="420" height="315" src="//www.youtube.com/embed/'
        //    + linkText
        //    + '" frameborder="0" allowfullscreen></iframe>'
        //    + '</div>';

        var imgUrl = 'http://img.youtube.com/vi/' + linkText + '/hqdefault.jpg';

        html += '<div class="youtube-preview" style="'
            + 'background-image: url('
            + imgUrl +
            ');">'
            + '<a class="play-button" href="#'
            + linkText
            + '"><i class="fa fa-4x fa-youtube-play"></i></a>'
            + '<img class="img-responsive" src="'
            + imgUrl
            + '" style="visibility:hidden"/></div>';
    };

    var buildSoundCloudLink = function() {
        isSoundCloudLink = false;

        html += ''
            + '<iframe width="100%" height="166" scrolling="no" frameborder="no" '
            + 'src="https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/'
            + linkText
            + '&amp;color=ff9900&amp;auto_play=false&amp;hide_related=true&amp;show_comments=false&amp;show_user=true&amp;show_reposts=false">'
            + '</iframe>';
    };

    var buildVimeoLink = function() {
        isVimeoLink = false;

        html += '<div class="flex-video vimeo widescreen">'
            + '<iframe src="//player.vimeo.com/video/'
            + linkText
            + '?title=0&amp;byline=0&amp;portrait=0&amp;badge=0&amp;color=ffffff" width="500" height="213" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>'
            + '</div>';
    };

    var buildImgLink = function() {
        html += '<img class="img-responsive" src="'
            + linkText.trim()
            + '" />';
    };

    var buildLink = function (index) {
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
    };

    this.build = function(text) {
        html = '';
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

                    if (isYoutubeLink) {
                        buildYoutubeLink();
                    } else if (isSoundCloudLink) {
                        buildSoundCloudLink();
                    } else if (isVimeoLink) {
                        buildVimeoLink();
                    } else {
                        var index = linkText.lastIndexOf('|');

                        if (index < 1) {
                            buildImgLink();
                        } else {
                            buildLink(index);
                        }
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

                if (linkText == 'youtube:') {
                    linkText = '';
                    isYoutubeLink = true;
                } else if (linkText == 'soundcloud:') {
                    linkText = '';
                    isSoundCloudLink = true;
                } else if (linkText == 'vimeo:') {
                    linkText = '';
                    isVimeoLink = true;
                }
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
    };
}

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

$(function () {
    $(document).on('click', '.play-button', function (e) {
        var $parent = $(this).parents('.youtube-preview');
        var videoId = $(this).attr('href').substring(1);

        $parent.after(
            '<div class="flex-video widescreen">'
            + '<iframe src="//www.youtube.com/embed/'
            + videoId
            + '?autoplay=1" frameborder="0" allowfullscreen></iframe>'
            + '</div>');

        $parent.remove();

        e.preventDefault();
        return false;
    });
});