var testApp = angular.module('testApp', ['ngRoute', 'ngAnimate']);

// ROUTING ===============================================
// set our routing for this application
// each route will pull in a different controller
testApp.config(function ($routeProvider) {
    $routeProvider
    	// home page
    	.when('/', {
    	    templateUrl: 'templates/test.html',
    	    controller: 'testController'
    	})

    	// contact page
    	.when('/signin', {
    	    templateUrl: 'templates/signin.html',
    	    controller: 'signinController'
    	})

    	// about page
    	.when('/signup', {
    	    templateUrl: 'templates/signup.html',
    	    controller: 'signupController'
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
testApp.controller('signinController', function ($scope) {
    $scope.pageClass = 'page-about';
	$scope.btnClicked = function(){
		console.log("ali");
		$scope.name = "ali";
	};
});

// contact page controller
testApp.controller('signupController', function ($scope) {
    $scope.pageClass = 'page-contact';
});