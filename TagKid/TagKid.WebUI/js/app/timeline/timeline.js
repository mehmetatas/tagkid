app.controller('TimelineCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.Tags = [
       { Name: 'c#', Hint: 'c-sharp', Description: 'programming language' },
       { Name: 'java', Hint: 'java', Description: 'open source programming language' },
       { Name: 'javascript', Hint: 'jscript', Description: 'scripting language' },
       { Name: 'php', Hint: 'php Hint', Description: 'php desc' },
       { Name: 'sql-server', Hint: 'microsoft', Description: 'ms sql server desc' },
       { Name: 'oracle', Hint: '12g', Description: 'oracle desc' },
       { Name: 'mysql', Hint: 'db', Description: 'mysql desc' },
       { Name: 'phyton', Hint: 'snake', Description: 'piton desc' },
       { Name: 'ruby-on-rails', Hint: 'ruby', Description: 'ruby on rails desc' },
       { Name: 'objective-c', Hint: 'ios', Description: 'objective c desc' }
    ];

    $scope.SelectedTags = [];

    $scope.removeTag = function(tag, e) {
        var tags = $scope.SelectedTags;
        for (var i = 0; i < tags.length; i++) {
            if (tags[i].Name == tag.Name) {
                tags.splice(i, 1);
                break;
            }
        }

        setTimeout(function() {
            $('.tag-input').focus();
        }, 100);

        return false;
    };

    var allowedChars = 'abcdefghijklmnopqrstuvwxyz1234567890-.+#';

    $('.tag-input').keypress(function (e) {
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
                $scope.SelectedTags.push({ Id: 1, Name: text });
                $scope.$apply();
                $element.val('');

            } else {
                $element.val(text);
            }

            e.preventDefault();
        } else if (code == 32 || c == ' ') {
            c = '-';
            if (caretPos != 0 && text.charAt(caretPos - 1) != c && text.charAt(caretPos) != c) {
                text = text.substr(0, caretPos) + c + text.substr(caretPos);
                $element.val(text.toLowerCase()).setCaretPosition(caretPos + 1);
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
        .on('keydown', function (e) {
            var $element = $(this);

            if (e.keyCode == 8) { // backspace
                if ($element.val().length == 0 && $scope.SelectedTags.length > 0) {
                    $scope.SelectedTags.splice($scope.SelectedTags.length - 1, 1);
                    $scope.$apply();

                    return;
                }
            }
        })
        .on('change keyup paste', function (e) {
            var $element = $(this);

            setTimeout(function () {
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
                }
            }, 50);
        });
}]);

app.filter('propsFilter', function() {
    return function(items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function(item) {
                var itemMatches = false;

                var keys = Object.keys(props);
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});


(function ($) {
    $.fn.getCaretPosition = function () {
        var input = this.get(0);

        if (!input)
            return -1;

        if ('selectionStart' in input) {
            return input.selectionStart;
        } else if (document.selection) {
            input.focus();
            var sel = document.selection.createRange();
            var selLen = document.selection.createRange().text.length;
            sel.moveStart('character', -input.value.length);
            return sel.text.length - selLen;
        }
        return -1;
    };

    $.fn.setCaretPosition = function (caretPos) {
        var input = this.get(0);

        if (!input)
            return;

        if (input.createTextRange) {
            var range = input.createTextRange();
            range.move('character', caretPos);
            range.select();
        } else {
            if (input.selectionStart) {
                input.focus();
                input.setSelectionRange(caretPos, caretPos);
            } else {
                input.focus();
            }
        }
    };
})(jQuery);