app.controller('SignInCtrl', [
    '$scope', 'auth', function ($scope, auth) {
        $scope.signin = function () {
            auth.signInWithPassword($scope.req);
        };
    }
]);