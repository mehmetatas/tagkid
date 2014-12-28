app.controller('EditorCtrl', [
    '$scope', '$modal', 'tagkid', 'post', function ($scope, $modal, tagkid, post) {
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

        $scope.tags = [
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
        post.getCategories(function (resp) {
            $scope.categories = resp.Data;
        });

        $scope.selectCategory = function (cat) {
            $scope.post.Category = cat;
        };
        $scope.showNewCategoryPopup = function () {
            $modal.open({
                templateUrl: 'newCategoryModalContent.html',
                controller: 'NewCategoryModalCtrl'
            }).result.then(function (newCategory) {
                post.createCategory({ Category: newCategory }, function(resp) {
                    newCategory.Id = resp.Data;
                    $scope.categories.push(newCategory);
                    $scope.post.Category = newCategory;
                });
            });
        };

        $scope.saveAsDraft = function () {
            post.saveAsDraft({
                Post: $scope.post
            });
        };

        $scope.cancel = function () {
            alert('cancel');
        };

        $scope.publish = function () {
            alert('publish');
        };
    }
]);