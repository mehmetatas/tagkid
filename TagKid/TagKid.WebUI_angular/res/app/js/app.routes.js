'use strict';

angular.module('app')
    .run([
        '$rootScope', '$state', '$stateParams', function($rootScope, $state, $stateParams) {
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
        }
    ])
    .config([
        '$stateProvider', '$urlRouterProvider', '$locationProvider', function($stateProvider, $urlRouterProvider, $locationProvider) {

            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false
            });

            $urlRouterProvider
                .otherwise('/');

            $stateProvider
                .state('pages', {
                    abstract: true,
                    url: '',
                    templateUrl: '/Layout/Main'
                })
                .state('pages.timeline', {
                    url: '/',
                    templateUrl: '/Pages/Timeline',
                    controller: 'timelineCtrl'
                });
        }
    ]);