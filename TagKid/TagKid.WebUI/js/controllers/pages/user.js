app.controller('UserCtrl', [
    '$scope', '$modal', '$stateParams', function ($scope, $modal, $stateParams) {
        $scope.username = $stateParams.username;
    }]);