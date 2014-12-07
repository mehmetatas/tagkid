
app.controller('NewCategoryModalCtrl', [
    '$scope', '$modalInstance', function ($scope, $modalInstance) {
        $scope.newCategory = {
            Name: '',
            Description: '',
            CssClass: 'bg-light',
            ColorName: 'Gray'
        };

        $scope.newCategoryColors = [
            { CssClass: 'bg-light', Name: 'Gray' },
            { CssClass: 'bg-dark', Name: 'Dark' },
            { CssClass: 'bg-black', Name: 'Black' },
            { CssClass: 'bg-primary', Name: 'Purple' },
            { CssClass: 'bg-info', Name: 'Blue' },
            { CssClass: 'bg-success', Name: 'Green' },
            { CssClass: 'bg-warning', Name: 'Yellow' },
            { CssClass: 'bg-danger', Name: 'Red' }
        ];

        $scope.selectNewCategoryColor = function (color) {
            $scope.newCategory.CssClass = color.CssClass;
            $scope.newCategory.ColorName = color.Name;
        };

        $scope.cancelNewCategory = function () {
            $modalInstance.dismiss('cancel');
        };

        $scope.saveNewCategory = function () {
            $modalInstance.close($scope.newCategory);
        };
    }
]);
