
tagkidApp.controller('signupController', function ($scope, $http, $location, authService) {
    tagkid.setContext($scope, $http, $location, authService);

    $scope.pageClass = 'signup';

    $scope.onSignInClick = function () {
        $location.path("/signin");
    };

    $scope.onSignUpEmailClick = function () {
        $http.post('/api/auth/signup_email', {
            email: $scope.email,
            username: $scope.username,
            password: $scope.password
        }).success(function (resp) {
            if (resp.responseCode == 0)
                alert('success');
            else
                alert(resp.responseMessage);
        }).error(function (err) {
            alert(err);
        });
    };

    $scope.onSignUpFacebookClick = function () {
        $http.post('/api/auth/signup_facebook', {
            email: $scope.email,
            username: $scope.username,
            password: $scope.password
        }).success(function (resp) {
            if (resp.responseCode == 0)
                alert('success');
            else
                alert(resp.responseMessage);
        }).error(function (err) {
            alert(err);
        });
    };

    tagkid.redirectToDashboardIfLoggedIn();
});
