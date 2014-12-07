/// <reference path="app/timeline/timeline.js" />
/// <reference path="app/timeline/timeline.js" />
/// <reference path="app/timeline/timeline.js" />
'use strict';

/**
 * Config for the router
 */
angular.module('app')
  .run(
    ['$rootScope', '$state', '$stateParams',
      function ($rootScope, $state, $stateParams) {
          $rootScope.$state = $state;
          $rootScope.$stateParams = $stateParams;
      }
    ]
  )
  .config(
    ['$stateProvider', '$urlRouterProvider', '$locationProvider',
      function ($stateProvider, $urlRouterProvider, $locationProvider) {

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
                  templateUrl: 'layout/main'
              })
              .state('pages.timeline', {
                  url: '/',
                  templateUrl: 'pages/timeline',
                  controller: 'TimelineCtrl',
                  //resolve: {
                  //    deps: [
                  //        '$ocLazyLoad',
                  //        function ($ocLazyLoad) {
                  //            $ocLazyLoad.load('timeline');
                  //        }
                  //    ]
                  //}
              })
              .state('access', {
                    url: '',
                    template: '<div ui-view class="fade-in-right-big smooth"></div>'
              })
              .state('access.signup', {
                  url: '/signup',
                  templateUrl: 'pages/signup',
                  controller: 'SignUpCtrl',
              })
              .state('access.signin', {
                  url: '/signin',
                  templateUrl: 'pages/signin',
                  controller: 'SignInCtrl',
              })
          .state('access.forgotpwd', {
              url: '/forgotpwd',
              templateUrl: 'pages/forgotpwd',
              controller: 'ForgotPwdCtrl',
          });
      }
    ]
  );