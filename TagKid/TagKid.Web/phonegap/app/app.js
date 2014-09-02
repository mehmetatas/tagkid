var testApp = angular.module('testApp', ['ngRoute', 'ngAnimate', 'ngTouch']);

// ROUTING ===============================================
testApp.config(function ($routeProvider) {
    $routeProvider
    	.when('/', {
    	    templateUrl: 'templates/signup.html',
    	    controller: 'signupController'
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
            email_or_username: $scope.email_or_username,
            password: $scope.password
        }).success(function (resp) {
            if (resp.code == 0)
                alert('success');
            else
                alert(resp.message);
        }).error(function () {
            alert('error');
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
                alert(resp.message);
        }).error(function () {
            alert('error');
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
                alert(resp.message);
        }).error(function () {
            alert('error');
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
                alert(resp.message);
        }).error(function () {
            alert('error');
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
                alert(resp.message);
        }).error(function () {
            alert('error');
        });
    };
});

testApp.controller('wallController', function ($scope) {
    $scope.pageClass = 'wall';
});