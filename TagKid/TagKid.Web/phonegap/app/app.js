var testApp = angular.module('testApp', ['ngRoute', 'ngAnimate', 'ngTouch']);

// ROUTING ===============================================
testApp.config(function ($routeProvider) {
    $routeProvider
    	.when('/', {
    	    templateUrl: 'templates/wall.html',
    	    controller: 'wallController'
    	})
    	.when('/signin', {
    	    templateUrl: 'templates/signin.html',
    	    controller: 'signinController'
    	})
    	.when('/signup', {
    	    templateUrl: 'templates/signup.html',
    	    controller: 'signupController'
    	})
		.otherwise({ redirectTo: '/' });
});


// CONTROLLERS ============================================

testApp.controller('signinController', function ($scope, $location, $http) {
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
            if (resp.code == 0)
                alert('success');
            else
                alert(resp.responseMessage);
        }).error(function (err) {
            alert(err);
        });
    };

    $scope.onSignInFacebookClick = function() {
        $http.post('/api/auth/signin_facebook', {
            email: $scope.email,
            username: $scope.username,
            password: $scope.password
        }).success(function (resp) {
            if (resp.code == 0)
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
            if (resp.code == 0)
                alert('success');
            else
                alert(resp.responseMessage);
        }).error(function (err) {
            alert(err);
        });
    };
});

testApp.controller('signupController', function ($scope, $location, $http) {
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
            if (resp.code == 0)
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
            if (resp.code == 0)
                alert('success');
            else
                alert(resp.responseMessage);
        }).error(function (err) {
            alert(err);
        });
    };
});

testApp.controller('wallController', function ($scope) {
    $scope.pageClass = 'wall';
});