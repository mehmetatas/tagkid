app.controller('SignInCtrl', [
    '$scope', '$modal', '$http', '$state', function($scope, $modal, $http, $state) {
        $scope.signin = function () {
            tagkid.auth.signInWithPassword($scope.req);
        };
    }
]);