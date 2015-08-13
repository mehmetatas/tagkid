app.config([
    '$stateProvider', '$urlRouterProvider', '$controllerProvider', '$compileProvider', '$locationProvider', '$filterProvider', '$provide', '$ocLazyLoadProvider', 'appDependencies',
    function ($stateProvider, $urlRouterProvider, $controllerProvider, $compileProvider, $locationProvider, $filterProvider, $provide, $ocLazyLoadProvider, appDependencies) {
        'use strict';

        app.controller = $controllerProvider.register;
        app.directive = $compileProvider.directive;
        app.filter = $filterProvider.register;
        app.factory = $provide.factory;
        app.service = $provide.service;
        app.constant = $provide.constant;
        app.value = $provide.value;

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });

        // LAZY LOAD MODULES
        // ----------------------------------- 

        $ocLazyLoadProvider.config({
            debug: false,
            events: true,
            modules: appDependencies.modules
        });

        // default route to dashboard
        $urlRouterProvider.otherwise('/mailbox/inbox');

        // 
        // app Routes
        // -----------------------------------   
        $stateProvider
                .state('app', {
                    url: '',
                    abstract: true,
                    templateUrl: basepath('app.html'),
                    controller: 'appCtrl',
                    resolve: requireDeps('icons', 'slimscroll', 'toaster', 'animate')
                })
                // Mailbox
                // ----------------------------------- 
                .state('app.mailbox', {
                    url: '/mailbox/:folder',
                    templateUrl: basepath('mailbox.html'),
                    controller: 'mailboxCtrl',
                    resolve: requireDeps('moment')
                })
                .state('app.mailbox.view', {
                    url: '/:id',
                    views: {
                        'mails@app.mailbox': {
                            controller: 'mailViewCtrl',
                            templateUrl: basepath('mailbox-view-mail.html')
                        }
                    }
                });


        // Change here your views base path
        function basepath(uri) {
            return '/app/html/' + uri;
        }

        // Generates a resolve object by passing script names
        // previously configured in constant.appDependencies
        // Also accept functions that returns a promise
        function requireDeps() {
            var _args = arguments;
            return {
                deps: [
                    '$ocLazyLoad', '$q', function ($ocLL, $q) {
                        // Creates a promise chain for each argument
                        var promise = $q.when(1); // empty promise
                        for (var i = 0, len = _args.length; i < len; i++) {
                            promise = addThen(_args[i]);
                        }
                        return promise;

                        // creates promise to chain dynamically
                        function addThen(_arg) {
                            // also support a function that returns a promise
                            if (typeof _arg == 'function')
                                return promise.then(_arg);
                            else
                                return promise.then(function () {
                                    // if is a module, pass the name. If not, pass the array
                                    var whatToLoad = getRequired(_arg);
                                    // simple error check
                                    if (!whatToLoad) return $.error('Route resolve: Bad resource name [' + _arg + ']');
                                    // finally, return a promise
                                    return $ocLL.load(whatToLoad);
                                });
                        }

                        // check and returns required data
                        // analyze module items with the form [name: '', files: []]
                        // and also simple array of script files (for not angular js)
                        function getRequired(name) {
                            if (appDependencies.modules)
                                for (var m in appDependencies.modules)
                                    if (appDependencies.modules[m].name && appDependencies.modules[m].name === name)
                                        return appDependencies.modules[m];
                            return appDependencies.scripts && appDependencies.scripts[name];
                        }

                    }
                ]
            };
        }


    }
]).config([
    '$tooltipProvider', function ($tooltipProvider) {
        $tooltipProvider.options({ appendToBody: true });
    }
]).config([
    'cfpLoadingBarProvider', function (cfpLoadingBarProvider) {
        cfpLoadingBarProvider.includeBar = true;
        cfpLoadingBarProvider.includeSpinner = false;
        cfpLoadingBarProvider.latencyThreshold = 500;
        cfpLoadingBarProvider.parentSelector = '.app-container > section';
    }
]);