
var app = angular.module('app', [
    'ngRoute',
    'ngAnimate',
    'ngStorage',
    'ngCookies',
    'pascalprecht.translate',
    'ui.bootstrap',
    'ui.router',
    'oc.lazyLoad',
    'cfp.loadingBar',
    'ui.utils'
]);

app.run([
    "$rootScope", "$state", "$stateParams", '$localStorage',
    function($rootScope, $state, $stateParams, $localStorage) {
        // Set reference to access them from any scope
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
        $rootScope.$storage = $localStorage;

        // Scope Globals
        // ----------------------------------- 
        $rootScope.app = {
            name: 'tagkid',
            year: new Date().getFullYear(),
            description: 'tag your knowledge!',
            layout: {
                isBoxed: true
            },
            sidebar: {
                isCollapsed: false
            }
        };

        // User information
        $rootScope.user = {
            name: 'Jimmie Stevens',
            job: 'Developer',
            picture: 'app/img/user/02.jpg'
        };
    }
]);
