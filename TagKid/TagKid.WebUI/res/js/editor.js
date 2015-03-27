var tkEditor = {
    create: function (input, preview, title) {
        var sync = function () {
            var htmlBuilder = new HtmlBuilder();
            var html = htmlBuilder.build($(input).val());

            if ($(title).val().length > 0) {
                html = '<h4>' + $(title).val() + '</h4>' + html;
            }

            $(preview).html(html).css('white-space', 'pre-wrap');
        };

        return {
            toggle: function () {
                sync();

                $(input).toggle();
                $(title).toggle();
                $(preview).toggle();
            }
        };
    }
};

var tkTagInput = {
    create: function(input, $scope) {
        var allowedChars = 'abcdefghijklmnopqrstuvwxyz1234567890-.+#';

        $(input).keypress(function(e) {
                var code = e.keyCode;
                if (e.charCode && code == 0)
                    code = e.charCode;
                var c = String.fromCharCode(code).toLowerCase();

                var $element = $(this);

                var caretPos = $element.getCaretPosition();

                var text = $element.val().toLowerCase();
                if (code == 13) {
                    while (text.charAt(text.length - 1) == '-' ||
                        text.charAt(text.length - 1) == '.')
                        text = text.substr(0, text.length - 1);

                    for (var i = 0; i < text.length; i++) {
                        if (allowedChars.indexOf(text.charAt(i)) < 0) {
                            text = text.substr(0, i) + text.substr(i + 1);
                            i--;
                        }
                    }

                    if (text.length > 0) {
                        $element.val('');
                        $scope.newPost.Tags.push({ Id: 0, Name: text });

                    } else {
                        $element.val(text);
                    }

                    $scope.$apply();
                    e.preventDefault();
                } else if (code == 32 || c == ' ') {
                    c = '-';
                    if (caretPos != 0 && text.charAt(caretPos - 1) != c && text.charAt(caretPos) != c) {
                        text = text.substr(0, caretPos) + c + text.substr(caretPos);
                        $element.val(text.toLowerCase()).setCaretPosition(caretPos + 1);
                        $scope.$apply();
                    }
                    e.preventDefault();
                } else if (allowedChars.indexOf(c) == -1 && code != 8 && code != 37 && code != 39 && code != 46) {
         
                    e.preventDefault();
                } else {
                    if (c === '-') {
                        if (caretPos == 0 || text.charAt(caretPos - 1) == c || text.charAt(caretPos) == c)
                            e.preventDefault();
                    }
                    //else if (c === '.') {
                    //    if (text.charAt(caretPos - 1) == c || text.charAt(caretPos) == c)
                    //        e.preventDefault();
                    //}
                }
            })
            .on('keydown', function(e) {
                var $element = $(this);

                if (e.keyCode == 8) { // backspace
                    if ($element.val().length == 0 && $scope.newPost.Tags.length > 0) {
                        $scope.newPost.Tags.splice($scope.newPost.Tags.length - 1, 1);
                        $scope.$apply();

                        return;
                    }
                }
            })
            .on('change keyup paste', function(e) {
                var $element = $(this);

                setTimeout(function() {
                    var text = $element.val();
                    text = text.toLowerCase();

                    for (var i = 0; i < text.length; i++) {
                        if (allowedChars.indexOf(text.charAt(i)) < 0) {
                            text = text.substr(0, i) + text.substr(i + 1);
                            i--;
                        }
                    }

                    var len;

                    do {
                        len = text.length;

                        while (text.charAt(0) == '-') {
                            text = text.substr(1);
                        }

                        while (text.indexOf('--') > -1) {
                            text = text.replace(/--/g, '-');
                        }

                        //while (text.indexOf('\.\.') > -1) {
                        //    text = text.replace(/\.\./g, '.');
                        //}

                        //while (text.indexOf('\.-') > -1) {
                        //    text = text.replace(/\.-/g, '');
                        //}

                        //while (text.indexOf('-\.-') > -1) {
                        //    text = text.replace(/-\.-/g, '-');
                        //}
                    } while (len != text.length);

                    if (text !== $element.val()) {
                        var caretPos = $element.getCaretPosition();
                        $element.val(text);
                        $element.setCaretPosition(caretPos);
                        $scope.$apply();
                    }
                }, 50);
            });
    }
};

$(function () {
    $(document).on('click', '.play-button', function (e) {
        var $parent = $(this).parents('.youtube-preview');
        var videoId = $(this).attr('data-href');

        $parent.after(
            '<div class="flex-video widescreen">'
            + '<iframe src="//www.youtube.com/embed/'
            + videoId
            + '?autoplay=1&html5=1" frameborder="0" allowfullscreen></iframe>'
            + '</div>');

        $parent.remove();

        e.preventDefault();
        return false;
    });
});
