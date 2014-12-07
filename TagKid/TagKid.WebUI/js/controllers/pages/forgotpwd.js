app.controller('ForgotPwdCtrl', [
    '$scope', '$modal', '$http', 'authService', function ($scope, $modal, $http, authService) {
        authService.redirectIfLoggedIn();

        $scope.isCollapsed = true;
    }
]);