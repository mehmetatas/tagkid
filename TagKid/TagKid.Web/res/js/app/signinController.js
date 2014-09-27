
tagkidApp.controller('signinController', function ($scope, $http, $location, authService) {
    tagkid.setContext($scope, $http, $location, authService);

    $scope.pageClass = 'signin';
    $scope.btnClicked = function () {
        console.log("ali");
        $scope.name = "ali";
    };

    $scope.onSignUpClick = function () {
        $location.path("/signup");
    };

    $scope.onSignInEmailClick = function () {
        $http.post('/api/auth/signin_email', {
            emailOrUsername: $scope.email_or_username,
            password: $scope.password
        }).success(function (resp) {
            if (resp.responseCode == 0) {
                tagkid.redirectToDashboard(resp);
            } else {
                alert(resp.responseMessage);
            }
        }).error(function (err) {
            alert(err);
        });
    };

    $scope.onSignInFacebookClick = function () {
        $http.post('/api/auth/signin_facebook', {
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

    $scope.onForgotPasswordClick = function () {
        $http.post('/api/auth/forgot_password', {
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