app.controller('sidebarCtrl', ['$rootScope', '$scope', '$location', '$http', '$timeout', 'appMediaquery', '$window',
  function ($rootScope, $scope, $location, $http, $timeout, appMediaquery, $window) {
      'use strict';

      $scope.app = $rootScope.app;

      $scope.menuItems = [
          {
              "text": "Inbox",
              "sref": "app.mailbox({folder: 'inbox'})",
              "icon": "fa-inbox",
              "alert": { "text": "3", "class": "label-success" }
          },
          {
              "text": "Suggested",
              "sref": "app.mailbox({folder: 'suggested'})",
              "icon": "fa-lightbulb-o",
              "alert": { "text": "14", "class": "label-success" }
          },
          {
              "text": "Favorites",
              "sref": "app.mailbox({folder: 'favorites'})",
              "icon": "fa-star-o",
              "alert": { "text": "21", "class": "label-success" }
          },
          {
              "text": "Drafts",
              "sref": "app.mailbox({folder: 'drafts'})",
              "icon": "fa-edit",
              "alert": { "text": "2", "class": "label-success" }
          },
          {
              "type": "separator"
          },
          {
              "text": "Microsoft",
              "sref": "#",
              "icon": "fa-windows",
              "alert": { "text": "12", "class": "label-success" },
              "subnav": [
                  { "text": "C#", "sref": "app.mailbox({folder: 'csharp'})", "alert": { "text": "4", "class": "label-primary" } },
                  { "text": "ASP.NET", "sref": "app.mailbox({folder: 'aspnet'})", "alert": { "text": "1", "class": "label-primary" } },
                  { "text": "Sql Server", "sref": "app.mailbox({folder: 'sqlserver'})", "alert": { "text": "7", "class": "label-primary" } }
              ]
          },
          {
              "text": "Game Programming",
              "sref": "#",
              "icon": "fa-gamepad",
              "subnav": [
                  { "text": "LibGDX", "sref": "app.mailbox({folder: 'libgdx'})" },
                  { "text": "RoboVM", "sref": "app.mailbox({folder: 'robovm'})" },
                  { "text": "Open GL", "sref": "app.mailbox({folder: 'opengl'})" }
              ]
          }
      ];

      var currentState = $rootScope.$state.current.name;
      var $win = $($window);
      var $html = $('html');
      var $body = $('body');

      // Adjustment on route changes
      $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
          currentState = toState.name;
          // Hide sidebar automatically on mobile
          $('body.aside-toggled').removeClass('aside-toggled');

          $rootScope.$broadcast('closeSidebarMenu');
      });

      // Normalize state on resize to avoid multiple checks
      $win.on('resize', function () {
          if (isMobile())
              $body.removeClass('aside-collapsed');
          else
              $body.removeClass('aside-toggled');
      });

      $rootScope.$watch('app.sidebar.isCollapsed', function (newValue, oldValue) {
          // Close subnav when sidebar change from collapsed to normal
          $rootScope.$broadcast('closeSidebarMenu');
          $rootScope.$broadcast('closeSidebarSlide');
      });

      // Check item and children active state
      var isActive = function (item) {

          if (!item || !item.sref) return;

          var path = item.sref, prefix = '#';
          if (path === prefix) {
              var foundActive = false;
              angular.forEach(item.subnav, function (value, key) {
                  if (isActive(value)) foundActive = true;
              });
              return foundActive;
          }
          else {
              return (currentState === path);
          }
      };


      $scope.getSidebarItemClass = function (item) {
          return (item.type == 'heading' ? 'nav-heading' : '') +
                 (isActive(item) ? ' active' : '');
      };


      // Handle sidebar collapse items
      // ----------------------------------- 
      var collapseList = [];

      $scope.addCollapse = function ($index, item) {
          collapseList[$index] = true; //!isActive(item);
      };

      $scope.isCollapse = function ($index) {
          return collapseList[$index];
      };

      $scope.collapseAll = function () {
          collapseAllBut(-1);
      };

      $scope.toggleCollapse = function ($index) {

          // States that doesn't toggle drodopwn
          if ((isSidebarCollapsed() && !isMobile()) || isSidebarSlider()) return true;

          // make sure the item index exists
          if (typeof collapseList[$index] === undefined) return true;

          collapseAllBut($index);
          collapseList[$index] = !collapseList[$index];

          return true;

      };

      function collapseAllBut($index) {
          angular.forEach(collapseList, function (v, i) {
              if ($index !== i)
                  collapseList[i] = true;
          });
      }

      // Helper checks
      // ----------------------------------- 

      function isMobile() {
          return $win.width() < appMediaquery.tablet;
      }
      function isTouch() {
          return $html.hasClass('touch');
      }
      function isSidebarCollapsed() {
          return $rootScope.app.sidebar.isCollapsed;
      }
      function isSidebarToggled() {
          return $body.hasClass('aside-toggled');
      }
      function isSidebarSlider() {
          return $rootScope.app.sidebar.slide;
      }

      // mailbox folder
      $scope.mailboxFolders = [
        { name: 'inbox', count: 3, icon: 'fa-inbox' },
        { name: 'suggested', count: 8, icon: 'fa-lightbulb-o' },
        { name: 'favorited', count: 12, icon: 'fa-star-o' },
        { name: 'draft', count: 1, icon: 'fa-edit' }
      ];
  }]);
