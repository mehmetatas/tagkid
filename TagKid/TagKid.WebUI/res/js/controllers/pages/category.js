app.controller('CategoryCtrl', [
    '$scope', '$modal', '$stateParams', function ($scope, $modal, $stateParams) {
        $scope.username = $stateParams.username;
        $scope.name = $stateParams.name;
    }]);