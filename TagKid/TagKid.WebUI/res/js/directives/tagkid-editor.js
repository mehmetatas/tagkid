angular.module('app')
    .directive('tagkidEditor', ['$modal', '$timeout', 'tagkid', 'postService', function ($modal, $timeout, tagkid, postService) {
        var loadCategories = function () {
            postService.getCategories({ UserId: tagkid.user().Id }, function (resp) {
                categories.splice(0, categories.length);
                for (var i = 0; i < resp.Data.length; i++) {
                    categories.push(resp.Data[i]);
                }
            });
        }

        var editor = tkEditor.create('#tk-editor', '#tk-preview', '#tk-title');

        var post = {
            Title: '',
            EditorContent: '',
            Category: '',
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

        var categories = [];

        var selectCategory = function (cat) {
            post.Category = cat;
        };

        var showNewCategoryPopup = function () {
            $modal.open({
                templateUrl: 'newCategoryModalContent.html',
                controller: 'NewCategoryModalCtrl'
            }).result.then(function (newCategory) {
                postService.createCategory({ Category: newCategory }, function (resp) {
                    newCategory.Id = resp.Data;
                    categories.push(newCategory);
                    post.Category = newCategory;
                });
            });
        };

        var toggleEditor = function () {
            $('#tk-preview-btn').toggle();
            $('#tk-edit-btn').toggle();
            editor.toggle();
        };

        var cancel = function () {
            if (confirm('Are you sure you want to cancel changes?')) {
                post.Category = null;
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
                post.Category = null;
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
            scope.categories = categories;
            scope.selectCategory = selectCategory;
            scope.showNewCategoryPopup = showNewCategoryPopup;
            scope.cancel = cancel;
            scope.saveAsDraft = saveAsDraft;
            scope.publish = publish;

            tkTagInput.create('#tk-tag-input', scope);
            loadCategories();
        };

        return {
            restrict: 'E',
            link: linker,
            templateUrl: '/Directives/Editor'
        };
    }]);