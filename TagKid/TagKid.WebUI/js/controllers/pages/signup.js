app.controller('SignUpCtrl', [
    '$scope', '$modal', '$http', 'authService', function($scope, $modal, $http, authService) {
        authService.redirectIfLoggedIn();
    }
]);