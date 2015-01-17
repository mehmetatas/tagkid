app.controller('UserCtrl', [
    '$scope', '$modal', '$stateParams', function ($scope, $modal, $stateParams) {
        $scope.username = $stateParams.username;
    }]);

app.controller('CategoryCtrl', [
    '$scope', '$modal', '$stateParams', function ($scope, $modal, $stateParams) {
        $scope.id = $stateParams.id;
        $scope.name = $stateParams.name;
    }]);

app.controller('TagCtrl', [
    '$scope', '$modal', '$stateParams', function ($scope, $modal, $stateParams) {
        $scope.id = $stateParams.id;
        $scope.name = $stateParams.name;
    }]);