angular.module('app')
    .directive('tagkidTagSearch', ['$modal', '$timeout', function ($modal, $timeout) {
        var allowedChars = 'abcdefghijklmnopqrstuvwxyz1234567890-.+#';

        var linker = function (scope, element, attrs) {
            var $input = $(element).find('.tag-input');

            var fix = function () {
                var text = scope.tagFilter.toLowerCase();

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
                } while (len != text.length);

                if (text === scope.tagFilter) {
                    return;
                }

                var caretPos = $input.getCaretPosition();
                scope.tagFilter = text;
                $input.setCaretPosition(caretPos);
            };

            var timeoutPromise;

            var fixAndSearch = function (e) {
                if (scope.tagFilter.length < 1) {
                    scope.selected = -1;
                    scope.searchResults.splice(0, scope.searchResults.length);

                    return;
                }

                var code = e.keyCode;
                if (e.charCode && code == 0)
                    code = e.charCode;
                var c = String.fromCharCode(code).toLowerCase();

                if (allowedChars.indexOf(c) == -1 && code != 8) {
                    e.preventDefault();
                    return;
                }

                fix();
                //$timeout(fix, 50);

                if (timeoutPromise) {
                    $timeout.cancel(timeoutPromise);
                }

                timeoutPromise = $timeout(search, 250);
            };

            scope.selected = -1;
            scope.searchResults = [];

            var search = function () {
                scope.selected = -1;
                scope.searchResults.splice(0, scope.searchResults.length);

                //postService.searchTags({ TagName: scope.tagFilter }, function (resp) {
                //    var tagArr = resp.Data;

                var tagArr = [{ Id: 1, Name: 'tag1' }, { Id: 2, Name: 'tag2' }]
                for (var i = 0; i < tagArr.length; i++) {
                    var alreadyAdded = false;
                    for (var j = 0; j < scope.post.Tags.length; j++) {
                        if (scope.post.Tags[j].Name == tagArr[i].Name) {
                            alreadyAdded = true;
                        }
                    }

                    if (!alreadyAdded) {
                        scope.searchResults.push(tagArr[i]);
                    }
                }

                //});
            };

            scope.addTag = function (tag) {
                scope.post.Tags.push(tag);
                scope.tagFilter = '';
                scope.searchResults.splice(0, scope.searchResults.length);
                $timeout(function () {
                    $input.focus();
                }, 100);
            };

            scope.removeTag = function (tag) {
                var postTags = scope.post.Tags;
                for (var i = 0; i < postTags.length; i++) {
                    if (postTags[i].Name == tag.Name) {
                        postTags.splice(i, 1);
                        break;
                    }
                }

                $timeout(function () {
                    $input.focus();
                }, 100);
            };

            scope.keydown = function (e) {
                if (e.keyCode == 8) { // backspace
                    if (scope.tagFilter.length == 0 && scope.post.Tags.length > 0) {
                        scope.post.Tags.splice(scope.post.Tags.length - 1, 1);
                    }
                }

                else if (e.keyCode == 38) { // up
                    scope.selected--;
                    if (scope.selected < 0) {
                        scope.selected = scope.searchResults.length - 1;
                    }
                }
                else if (e.keyCode == 40) { // down
                    scope.selected++;
                }

                var selectedIndex = scope.selected % scope.searchResults.length;
                for (var i = 0; i < scope.searchResults.length; i++) {
                    scope.searchResults[i].selected = i == selectedIndex;
                }
            };

            scope.change = function () {
                $timeout(fix, 50);
            };

            scope.keyup = function (e) {
                fixAndSearch(e);
            };

            scope.paste = function () {
                fixAndSearch();
            };

            scope.keypress = function (e) {
                var code = e.keyCode;
                if (e.charCode && code == 0)
                    code = e.charCode;
                var c = String.fromCharCode(code).toLowerCase();

                var caretPos = $input.getCaretPosition();

                var text = scope.tagFilter.toLowerCase();
                if (code == 13) { // enter
                    for (var i = 0; i < scope.searchResults.length; i++) {
                        if (scope.searchResults[i].selected) {
                            scope.tagFilter = '';
                            scope.post.Tags.push({ Id: 0, Name: scope.searchResults[i].Name });
                            scope.searchResults.splice(0, scope.searchResults.length);
                            e.preventDefault();
                            return;
                        }
                    }

                    while (text.charAt(text.length - 1) == '-' || text.charAt(text.length - 1) == '.')
                        text = text.substr(0, text.length - 1);

                    for (var i = 0; i < text.length; i++) {
                        if (allowedChars.indexOf(text.charAt(i)) < 0) {
                            text = text.substr(0, i) + text.substr(i + 1);
                            i--;
                        }
                    }

                    if (text.length > 0) {
                        var tag = { Id: 0, Name: text };

                        for (var i = 0; i < scope.searchResults.length; i++) {
                            if (scope.searchResults[i].Name == tag.Name) {
                                tag = scope.searchResults[i];
                                break;
                            }
                        }

                        scope.addTag(tag);
                    } else {
                        scope.tagFilter = text;
                    }

                    e.preventDefault();
                } else if (code == 32 || c == ' ') {
                    c = '-';
                    if (caretPos != 0 && text.charAt(caretPos - 1) != c && text.charAt(caretPos) != c) {
                        text = text.substr(0, caretPos) + c + text.substr(caretPos);
                        scope.tagFilter = text.toLowerCase();
                        $input.setCaretPosition(caretPos + 1);
                    }
                    e.preventDefault();
                } else if (allowedChars.indexOf(c) == -1 && code != 8 && code != 37 && code != 39 && code != 46) {
                    e.preventDefault();
                } else {
                    if (c === '-') {
                        if (caretPos == 0 || text.charAt(caretPos - 1) == c || text.charAt(caretPos) == c)
                            e.preventDefault();
                    }
                }
            };
        };

        return {
            templateUrl: 'Directives/TagSearch',
            restrict: 'A',
            link: linker,
            scope: {
                post: '='
            }
        };
    }]);