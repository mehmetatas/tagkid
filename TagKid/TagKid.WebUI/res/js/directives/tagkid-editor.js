angular.module('app')
    .directive('tagkidEditor', ['$modal', '$timeout', 'tagkid', 'postService', function ($modal, $timeout, tagkid, postService) {

        var editor = tkEditor.create('#tk-editor', '#tk-preview', '#tk-title');

        var post = {
            Title: '',
            EditorContent: '',
            Tags: [],
            EditorType: 0
        };

        var removeTag = function (tag) {
            var postTags = post.Tags;
            for (var i = 0; i < postTags.length; i++) {
                if (postTags[i].Name == tag.Name) {
                    postTags.splice(i, 1);
                    break;
                }
            }

            setTimeout(function () {
                $('#tk-tag-input').focus();
            }, 100);

            return false;
        };

        var toggleEditor = function () {
            $('#tk-preview-btn').toggle();
            $('#tk-edit-btn').toggle();
            editor.toggle();
        };

        var cancel = function () {
            if (confirm('Are you sure you want to cancel changes?')) {
                post.Tags = [];
                post.Title = '';
                post.EditorContent = '';
            }
        };

        var saveAsDraft = function () {
            postService.saveAsDraft({
                Post: post
            });
        };

        var publish = function () {
            postService.publish({
                Post: post
            }, function() {
                post.Tags = [];
                post.Title = '';
                post.EditorContent = '';
            });
        };

        var linker = function (scope, element, attrs) {
            scope.user = tagkid.user();
            scope.post = post;
            scope.toggleEditor = toggleEditor;
            scope.removeTag = removeTag;
            scope.cancel = cancel;
            scope.saveAsDraft = saveAsDraft;
            scope.publish = publish;

            tkTagInput.create('#tk-tag-input', scope);
        };

        return {
            restrict: 'E',
            link: linker,
            templateUrl: '/Directives/Editor'
        };
    }]);