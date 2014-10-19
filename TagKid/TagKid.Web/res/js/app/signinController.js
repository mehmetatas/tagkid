
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
            EmailOrUsername: $scope.email_or_username,
            Password: $scope.password
        }).success(function (resp) {
            if (resp.ResponseCode == 0) {
                tagkid.redirectToDashboard(resp);
            } else {
                alert(resp.ResponseMessage);
            }
        }).error(function (err) {
            alert(err);
        });
    };

    $scope.onSignInFacebookClick = function () {
        $http.post('/api/auth/signin_facebook', {
            Email: $scope.email,
            Username: $scope.username,
            Password: $scope.password
        }).success(function (resp) {
            if (resp.ResponseCode == 0)
                alert('success');
            else
                alert(resp.ResponseMessage);
        }).error(function (err) {
            alert(err);
        });
    };

    $scope.onForgotPasswordClick = function () {
        alert("not implemented!");
    };

    tagkid.redirectToDashboardIfLoggedIn();
});