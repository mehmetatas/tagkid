app.controller('SignInCtrl', [
    '$scope', '$modal', '$http', 'authService', function($scope, $modal, $http, authService) {
        authService.redirectIfLoggedIn();
    }
]);