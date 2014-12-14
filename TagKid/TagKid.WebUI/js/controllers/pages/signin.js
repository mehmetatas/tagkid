app.controller('SignInCtrl', [
    '$scope', function ($scope) {
        $scope.signin = function () {
            tagkid.auth.signInWithPassword($scope.req);
        };
    }
]);