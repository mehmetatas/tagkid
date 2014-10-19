
tagkidApp.controller('signupController', function($scope, $http, $location, authService) {
    tagkid.setContext($scope, $http, $location, authService);

    $scope.pageClass = 'signup';

    $scope.onSignInClick = function() {
        $location.path("/signin");
    };

    $scope.onSignUpEmailClick = function() {
        $http.post('/api/auth/signup_email', {
            Email: $scope.email,
            Username: $scope.username,
            Password: $scope.password
        }).success(function(resp) {
            if (resp.ResponseCode == 0)
                alert('success');
            else
                alert(resp.ResponseMessage);
        }).error(function(err) {
            alert(err);
        });
    };

    $scope.onSignUpFacebookClick = function() {
        $http.post('/api/auth/signup_facebook', {
            Email: $scope.email,
            Username: $scope.username,
            Password: $scope.password
        }).success(function(resp) {
            if (resp.ResponseCode == 0)
                alert('success');
            else
                alert(resp.ResponseMessage);
        }).error(function(err) {
            alert(err);
        });
    };

    tagkid.redirectToDashboardIfLoggedIn();
});