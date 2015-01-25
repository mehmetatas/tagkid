app.controller('EditorCtrl', [
    '$scope', '$modal', 'tagkid', 'postService', function ($scope, $modal, tagkid, postService) {
        postService.getCategories(function (resp) {
            $scope.categories = resp.Data;
        });

        var editor = tkEditor.create('#tk-editor', '#tk-preview', '#tk-title');

        tkTagInput.create('#tk-tag-input', $scope);

        $scope.toggleEditor = function () {
            $('#tk-preview-btn').toggle();
            $('#tk-edit-btn').toggle();
            editor.toggle();
        };

        $scope.user = tagkid.user();

        $scope.post = {
            Title: '',
            EditorContent: '',
            Category: '',
            Tags: [],
            EditorType: 0
        };

        $scope.tags = [];

        $scope.removeTag = function (tag) {
            var tags = $scope.post.Tags;
            for (var i = 0; i < tags.length; i++) {
                if (tags[i].Name == tag.Name) {
                    tags.splice(i, 1);
                    break;
                }
            }

            setTimeout(function () {
                $('#tk-tag-input').focus();
            }, 100);

            return false;
        };

        $scope.categories = [];

        $scope.selectCategory = function (cat) {
            $scope.post.Category = cat;
        };

        $scope.showNewCategoryPopup = function () {
            $modal.open({
                templateUrl: 'newCategoryModalContent.html',
                controller: 'NewCategoryModalCtrl'
            }).result.then(function (newCategory) {
                postService.createCategory({ Category: newCategory }, function (resp) {
                    newCategory.Id = resp.Data;
                    $scope.categories.push(newCategory);
                    $scope.post.Category = newCategory;
                });
            });
        };

        $scope.cancel = function () {
            if (confirm('Are you sure you want to cancel changes?')) {
                $scope.post.Category = null;
                $scope.post.Tags = [];
                $scope.post.Title = '';
                $scope.post.EditorContent = '';
            }
        };

        $scope.saveAsDraft = function () {
            postService.saveAsDraft({
                Post: $scope.post
            });
        };

        $scope.publish = function () {
            postService.publish({
                Post: $scope.post
            });
        };
    }
]);