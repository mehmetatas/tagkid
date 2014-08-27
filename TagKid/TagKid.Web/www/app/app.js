var testApp = angular.module('testApp', ['ngRoute', 'ngAnimate', 'ngTouch']);

// ROUTING ===============================================
// set our routing for this application
// each route will pull in a different controller
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
		.otherwise({redirectTo: '/'});
});


// CONTROLLERS ============================================
// home page controller
testApp.controller('testController', function ($scope, $location) {
    $scope.pageClass = 'page-home';
    $scope.name = "mehmet";
	
	
	$scope.btnClicked = function(){
		$location.path("/signin");
		//$scope.name = "ali";
	};
});

// about page controller
testApp.controller('signinController', function ($scope, $location) {
    $scope.pageClass = 'signin';
	$scope.btnClicked = function(){
		console.log("ali");
		$scope.name = "ali";
	};

	$scope.onSignUpClick = function () {
	    $location.path("/");
    };
});

// contact page controller
testApp.controller('signupController', function ($scope, $location) {
    $scope.pageClass = 'signup';
    
    $scope.onSignInClick = function () {
        $location.path("/signin");
    };
});