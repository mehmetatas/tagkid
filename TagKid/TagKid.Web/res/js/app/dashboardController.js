
tagkidApp.controller('dashboardController', function ($scope, $http, $location, authService) {
    tagkid.setContext($scope, $http, $location, authService);

    $scope.pageClass = 'dashboard';

    $scope.visibleTab = 'editor';

    $scope.tagFilter = '';

    $scope.tagSearchResults = [];

    $scope.post = {
        content: '',
        /* PostRequest */
        contentCode: '* Post 1 *',
        title: 'Title 1',
        tags: [],
        categoryId: 0,
        id: 0,
        retaggedPostId: 0,
        quoteAuthor: '',
        quoteText: '',
        mediaEmbedUrl: '',
        linkTitle: '',
        linkDescription: '',
        linkImageUrl: '',
        linkUrl: '',
        status: 0,
        type: 0,
        accessLevel: 0
    };

    $scope.onSignOutClick = function () {
        tagkid.signout();
    };

    $scope.format = function () {
        tgEditor.format();
    };

    $scope.searchTags = function () {
        $scope.tagSearchResults = [];
        $http.post('/api/post/search_tags', {
            Filter: $scope.tagFilter
        })
       .success(function (resp) {
           if (resp.ResponseCode == 0) {
               for (var i = 0; i < resp.Tags.length; i++)
                   $scope.tagSearchResults.push(resp.Tags[i]);
               $scope.$apply();
           } else {
               alert(resp.ResponseMessage);
           }
       }).error(function (err) {
           alert(err);
       });
    }

    $scope.savePost = function () {
        var post = $scope.post;

        $http.post('/api/post/save_post', {
            ContentCode: post.contentCode,
            Title: post.title,
            Tags: post.tags,
            CategoryId: post.categoryId,
            Id: post.id,
            RetaggedPostId: post.retaggedPostId,
            QuoteAuthor: post.quoteAuthor,
            QuoteText: post.quoteText,
            MediaEmbedUrl: post.mediaEmbedUrl,
            LinkTitle: post.linkTitle,
            LinkDescription: post.linkDescription,
            LinkImageUrl: post.linkImageUrl,
            LinkUrl: post.linkUrl,
            Status: post.status,
            Type: post.type,
            AccessLevel: post.accessLevel
        })
        .success(function (resp) {
            if (resp.ResponseCode == 0) {
                $('#newPostModal').modal('hide');
                tagkid.loadDashboard();
            } else {
                alert(resp.ResponseMessage);
            }
        }).error(function (err) {
            alert(err);
        });
    }

    $scope.openNewPostModal = function () {
        $('#newPostModal').modal('show').on('shown.bs.modal', function (e) {
            tgEditor.init();
        });
    };

    $scope.removeTag = function (tag) {
        var tags = $scope.post.tags;
        for (var i = 0; i < tags.length; i++) {
            if (tags[i].name == tag.name) {
                tags.splice(i, 1);
                break;
            }
        }

        setTimeout(function () {
            tgEditor.resize();
            $('.tag-input').focus();
        }, 100);
    }

    $('.editor-tab-buttons input').on('change', function () {
        $('.editor-tab-buttons input').each(function () {
            if ($(this).is(':checked')) {
                $scope.visibleTab = $(this).val();
                $scope.$apply();
            }
        });
    });

    var allowedChars = 'abcdefghijklmnopqrstuvwxyz1234567890-.+#';

    $('.tag-input')
        .keypress(function (e) {
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
                    $scope.post.tags.push({ id: 0, name: text });
                    $scope.$apply();
                    $element.val('');

                    tgEditor.resize();
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
                if ($element.val().length == 0 && $scope.post.tags.length > 0) {
                    $scope.post.tags.splice($scope.post.tags.length - 1, 1);
                    $scope.$apply();

                    tgEditor.resize();
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

    tagkid.ensureLoggedIn();
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