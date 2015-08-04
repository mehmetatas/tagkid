﻿app.service('sidebarMenu', ["$rootScope", function ($rootScope) {
    'use strict';
   
    return {
        load: function () {

            $rootScope.menuItems = [
                {
                    "text": "Main Navigation",
                    "type": "heading",
                    "translate": "sidebar.heading.HEADER"
                },
                {
                    "text": "Dashboard",
                    "sref": "app.dashboard",
                    "icon": "icon-bar-graph",
                    "translate": "sidebar.nav.DASHBOARD",
                    "alert": { "text": "3", "class": "label-info" }
                },
                {
                    "text": "Components",
                    "sref": "#",
                    "icon": "icon-box",
                    "subnav": [
                        { "text": "Buttons", "sref": "app.buttons", "translate": "sidebar.nav.component.BUTTON" },
                        { "text": "Notifications", "sref": "app.notifications", "translate": "sidebar.nav.component.NOTIFICATION" },
                        { "text": "Bootsrap UI", "sref": "app.bootstrapui", "translate": "sidebar.nav.component.BOOTSTRAPUI", "alert": { "text": "7", "class": "label-primary" } },
                        {
                            "type": "separator"
                        },
                        { "text": "Panels", "sref": "app.panels", "translate": "sidebar.nav.component.PANEL" },
                        { "text": "Portlets", "sref": "app.portlets", "translate": "sidebar.nav.component.PORTLET" },
                        {
                            "type": "separator"
                        },
                        { "text": "Grid", "sref": "app.grid", "translate": "sidebar.nav.component.GRID" },
                        { "text": "Grid Masonry", "sref": "app.grid-masonry", "translate": "sidebar.nav.component.GRID_MASONRY" },
                        { "text": "Typography", "sref": "app.typo", "translate": "sidebar.nav.component.TYPO" },
                        { "text": "Palette", "sref": "app.palette", "translate": "sidebar.nav.component.PALETTE" }
                    ],
                    "translate": "sidebar.nav.component.COMPONENTS"
                },
                {
                    "type": "separator"
                },
                {
                    "text": "Forms",
                    "sref": "#",
                    "icon": "icon-book",
                    "subnav": [
                        { "text": "Inputs", "sref": "app.form-inputs", "translate": "sidebar.nav.form.INPUT" },
                        { "text": "Validation", "sref": "app.form-validation", "translate": "sidebar.nav.form.VALIDATION" },
                        { "text": "Wizard", "sref": "app.form-wizard", "translate": "sidebar.nav.form.WIZARD" }
                    ],
                    "translate": "sidebar.nav.form.FORM"
                },
                {
                    "text": "Tables",
                    "sref": "#",
                    "icon": "icon-grid",
                    "subnav": [
                        { "text": "Responsive", "sref": "app.table-responsive", "translate": "sidebar.nav.table.RESPONSIVE" },
                        { "text": "ngTables", "sref": "app.table-ngtable" }
                    ],
                    "translate": "sidebar.nav.table.TABLE"
                },
                {
                    "text": "Charts",
                    "sref": "app.charts",
                    "icon": "icon-pie-graph",
                    "translate": "sidebar.nav.CHART",
                    "alert": { "text": "12", "class": "label-success" }
                },
                {
                    "text": "Maps",
                    "sref": "#",
                    "icon": "icon-map",
                    "subnav": [
                        { "text": "Google Maps", "sref": "app.maps-google", "translate": "sidebar.nav.map.GOOGLE" },
                        { "text": "Vector Maps", "sref": "app.maps-vector", "translate": "sidebar.nav.map.VECTOR" }
                    ],
                    "translate": "sidebar.nav.map.MAP"
                },
                {
                    "text": "Icons",
                    "sref": "#",
                    "icon": "icon-play",
                    "subnav": [
                        { "text": "Feather Icons", "sref": "app.icons-feather", "translate": "sidebar.nav.icon.FEATHER" },
                        { "text": "Font Awesome", "sref": "app.icons-fa", "alert": "+400", "translate": "sidebar.nav.icon.FA" },
                        { "text": "Climacons", "sref": "app.icons-climacon", "translate": "sidebar.nav.icon.CLIMA" },
                        { "text": "Weather Icons", "sref": "app.icons-weather", "alert": "+100", "translate": "sidebar.nav.icon.WEATHER" }
                    ],
                    "translate": "sidebar.nav.icon.ICON"
                },
                {
                    "type": "separator"
                },
                {
                    "text": "Extras",
                    "type": "heading"
                },
                {
                    "text": "Apps",
                    "sref": "#",
                    "icon": "icon-ribbon",
                    "subnav": [
                        { "text": "Mailbox", "sref": "app.mailbox.folder.list({folder: 'inbox'})", "translate": "sidebar.nav.app.MAILBOX" },
                        { "text": "Calendar", "sref": "app.calendar", "translate": "sidebar.nav.app.CALENDAR" },
                        { "text": "Tasks List", "sref": "app.tasks", "translate": "sidebar.nav.app.TASK" }
                    ],
                    "translate": "sidebar.nav.app.APP"
                },
                {
                    "text": "Pages",
                    "sref": "#",
                    "icon": "icon-stack",
                    "subnav": [
                        { "text": "Login", "sref": "page.login", "translate": "sidebar.nav.pages.LOGIN" },
                        { "text": "Sign up", "sref": "page.register", "translate": "sidebar.nav.pages.REGISTER" },
                        { "text": "Recover Password", "sref": "page.recover", "translate": "sidebar.nav.pages.RECOVER" },
                        { "text": "Lock", "sref": "page.lock", "translate": "sidebar.nav.pages.LOCK" },
                        { "text": "Starter Template", "sref": "app.template", "translate": "sidebar.nav.pages.BLANK" },
                        { "text": "Invoice", "sref": "app.invoice", "translate": "sidebar.nav.pages.INVOICE" },
                        { "text": "Price", "sref": "app.price", "translate": "sidebar.nav.pages.PRICE" },
                        { "text": "Search", "sref": "app.search", "translate": "sidebar.nav.pages.SEARCH" }
                    ],
                    "translate": "sidebar.nav.pages.PAGES"
                }
            ];
        }
    };

}]);