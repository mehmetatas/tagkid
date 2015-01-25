app.controller('TagCtrl', [
    '$scope', '$modal', '$stateParams', function ($scope, $modal, $stateParams) {
        $scope.name = $stateParams.name;
    }]);