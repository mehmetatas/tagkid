app.controller('EditorCtrl', [
    '$scope', '$modal', function ($scope, $modal) {
        var editor = tkEditor.create('#tk-editor', '#tk-preview', '#tk-title');

        tkTagInput.create('#tk-tag-input', $scope);

        $scope.toggleEditor = function () {
            $('#tk-preview-btn').toggle();
            $('#tk-edit-btn').toggle();
            editor.toggle();
        };

        $scope.user = tagkid.context.user;

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

        $scope.selectedTags = [];

        $scope.removeTag = function (tag) {
            var tags = $scope.selectedTags;
            for (var i = 0; i < tags.length; i++) {
                if (tags[i].Name == tag.Name) {
                    tags.splice(i, 1);
                    break;
                }
            }

            setTimeout(function () {
                $('.tag-input').focus();
            }, 100);

            return false;
        };

        $scope.selectedCategory = null;
        $scope.categories = [
            { Name: 'coding', CssClass: 'bg-danger' },
            { Name: 'daily', CssClass: 'bg-warning' },
            { Name: 'photography', CssClass: 'bg-success' },
            { Name: 'sports', CssClass: 'bg-info' }
        ];
        $scope.selectCategory = function (cat) {
            $scope.selectedCategory = cat;
        };
        $scope.showNewCategoryPopup = function () {
            $modal.open({
                templateUrl: 'newCategoryModalContent.html',
                controller: 'NewCategoryModalCtrl'
            }).result.then(function (newCategory) {
                $scope.categories.push(newCategory);
                $scope.selectedCategory = newCategory;
            });
        };

    }
]);