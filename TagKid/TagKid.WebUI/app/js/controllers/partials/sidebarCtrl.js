app.controller("sidebarCtrl", ["$rootScope", "$scope", "$location", "$http", "$timeout", "appMediaquery", "$window",
  function ($rootScope, $scope, $location, $http, $timeout, appMediaquery, $window) {
      "use strict";

      $scope.app = $rootScope.app;

      var loadMenuItems = function () {
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
              },
              {
                  "type": "separator"
              },
              {
                  "text": "Private Folder",
                  "sref": "#",
                  "icon": "fa-lock",
                  "alert": { "text": "12", "class": "label-success" },
                  "subnav": [
                      { "text": "Photography", "sref": "app.mailbox({folder: 'photography'})", "alert": { "text": "4", "class": "label-primary" } },
                      { "text": "Guitar", "sref": "app.mailbox({folder: 'guitar'})", "alert": { "text": "1", "class": "label-primary" } },
                      { "text": "Drums", "sref": "app.mailbox({folder: 'drums'})", "alert": { "text": "7", "class": "label-primary" } }
                  ]
              }
          ];
      };

      $timeout(loadMenuItems, 2000);

      $scope.editFolders = function() {
          alert("edit folders");
      };

      var currentState = $rootScope.$state.current.name;
      var $win = $($window);
      var $body = $("body");

      // Adjustment on route changes
      $rootScope.$on("$stateChangeStart", function (event, toState, toParams, fromState, fromParams) {
          currentState = toState.name;
          // Hide sidebar automatically on mobile
          $("body.aside-toggled").removeClass("aside-toggled");

          $rootScope.$broadcast("closeSidebarMenu");
      });

      // Normalize state on resize to avoid multiple checks
      $win.on("resize", function () {
          if (isMobile())
              $body.removeClass("aside-collapsed");
          else
              $body.removeClass("aside-toggled");
      });

      $rootScope.$watch("app.sidebar.isCollapsed", function (newValue, oldValue) {
          // Close subnav when sidebar change from collapsed to normal
          $rootScope.$broadcast("closeSidebarMenu");
      });

      // Check item and children active state
      var isActive = function (item) {
          if (!item || !item.sref)
              return;

          var path = item.sref, prefix = "#";
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
          return (item.type == "heading" ? "nav-heading" : "") +
                 (isActive(item) ? " active" : "");
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
          if ((isSidebarCollapsed() && !isMobile()))
              return true;

          // make sure the item index exists
          if (typeof collapseList[$index] === undefined)
              return true;

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

      function isSidebarCollapsed() {
          return $rootScope.app.sidebar.isCollapsed;
      }
  }]);
