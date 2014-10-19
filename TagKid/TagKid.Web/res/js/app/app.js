var tagkidApp = angular.module('tagkidApp', ['ngRoute', 'ngAnimate', 'ngTouch', 'ngSanitize']);

// ROUTING ===============================================
tagkidApp.config(function($routeProvider, $locationProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/templates/signin.html',
            controller: 'signinController'
        })
        .when('/signin', {
            templateUrl: '/templates/signin.html',
            controller: 'signinController'
        })
        .when('/signup', {
            templateUrl: '/templates/signup.html',
            controller: 'signupController'
        })
        .when('/dashboard', {
            templateUrl: '/templates/dashboard.html',
            controller: 'dashboardController'
        })
        .otherwise({ redirectTo: '/' });

    $locationProvider.html5Mode(true);
});

// SERVICES ===============================================

tagkidApp.service('authService', function() {
    this.user = null;
});