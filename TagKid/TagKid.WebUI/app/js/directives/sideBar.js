
app.directive('sideBar', ['$rootScope', '$window', '$timeout', '$compile', 'appMediaquery', 'support', '$http', '$templateCache',
    function ($rootScope, $window, $timeout, $compile, appMediaquery, support, $http, $templateCache) {
        'use strict';

        var $win = $($window);
        var $html = $('html');
        var $scope;
        var $sidebar;
        var $sidebarNav;
        var $sidebarButtons;

        return {
            restrict: 'E',
            template: '<nav class="sidebar" ng-transclude></nav>',
            transclude: true,
            replace: true,
            link: function (scope, element, attrs) {

                $scope = scope;
                $sidebar = element;
                $sidebarNav = element.children('.sidebar-nav');
                $sidebarButtons = element.find('.sidebar-buttons');

                // depends on device the event we use
                var eventName = isTouch() ? 'click' : 'mouseenter';
                $sidebarNav.on(eventName, '.nav > li', function () {

                    if (isSidebarCollapsed() && !isMobile()) {
                        toggleMenuItem($(this));
                        if (isTouch()) {
                            sidebarAddBackdrop();
                        }
                    }

                });

                // if something ask us to close the sidebar menu
                $scope.$on('closeSidebarMenu', function () {
                    sidebarCloseFloatItem();
                });
            }
        };

        // Handles hover to open items on 
        // collapsed menu
        // ----------------------------------- 
        function toggleMenuItem($listItem) {

            sidebarCloseFloatItem();

            var ul = $listItem.children('ul');

            if (!ul.length)
                return;

            var navHeader = $('.navbar-header');
            var mar = $rootScope.app.layout.isFixed ? parseInt(navHeader.outerHeight(true), 0) : 0;

            var subNav = ul.clone().appendTo('.sidebar-wrapper');

            var itemTop = ($listItem.position().top + mar) - $sidebar.scrollTop();
            var vwHeight = $win.height();

            subNav
              .addClass('nav-floating')
              .css({
                  position: $rootScope.app.layout.isFixed ? 'fixed' : 'absolute',
                  top: itemTop,
                  bottom: (subNav.outerHeight(true) + itemTop > vwHeight) ? 0 : 'auto'
              });

            subNav.on('mouseleave', function () {
                subNav.remove();
            });

        }

        function sidebarCloseFloatItem() {
            $('.dropdown-backdrop').remove();
            $('.sidebar-subnav.nav-floating').remove();
        }

        function sidebarAddBackdrop() {
            var $backdrop = $('<div/>', { 'class': 'dropdown-backdrop' });
            $backdrop.insertAfter($sidebar).on("click", function () {
                sidebarCloseFloatItem();
            });
        }
        
        function isTouch() {
            return $html.hasClass('touch');
        }

        function isSidebarCollapsed() {
            return $rootScope.app.sidebar.isCollapsed;
        }

        function isMobile() {
            return $win.width() < appMediaquery.tablet;
        }
    }]);
