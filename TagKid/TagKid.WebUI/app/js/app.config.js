app.config([
    '$stateProvider', '$urlRouterProvider', '$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$ocLazyLoadProvider', 'appDependencies',
    function($stateProvider, $urlRouterProvider, $controllerProvider, $compileProvider, $filterProvider, $provide, $ocLazyLoadProvider, appDependencies) {
        'use strict';

        app.controller = $controllerProvider.register;
        app.directive = $compileProvider.directive;
        app.filter = $filterProvider.register;
        app.factory = $provide.factory;
        app.service = $provide.service;
        app.constant = $provide.constant;
        app.value = $provide.value;

        // LAZY LOAD MODULES
        // ----------------------------------- 

        $ocLazyLoadProvider.config({
            debug: false,
            events: true,
            modules: appDependencies.modules
        });


        // default route to dashboard
        $urlRouterProvider.otherwise('/app/mailbox/inbox');

        // 
        // app Routes
        // -----------------------------------   
        $stateProvider
                // 
                // Single Page Routes
                // ----------------------------------- 
                .state('page', {
                    url: '/page',
                    templateUrl: basepath('pages/page.html'),
                    resolve: requireDeps('icons', 'animate')
                })
                .state('page.login', {
                    url: '/login',
                    templateUrl: basepath('pages/login.html')
                })
                .state('page.register', {
                    url: '/register',
                    templateUrl: basepath('pages/register.html')
                })
                .state('page.recover', {
                    url: '/recover',
                    templateUrl: basepath('pages/recover.html')
                })
                .state('page.lock', {
                    url: '/lock',
                    templateUrl: basepath('pages/lock.html')
                })
                // App
                // ----------------------------------- 
                .state('app', {
                    url: '/app',
                    abstract: true,
                    templateUrl: basepath('app.html'),
                    controller: 'appController',
                    resolve: requireDeps('icons', 'screenfull', 'sparklines', 'slimscroll', 'toaster', 'ui.knob', 'animate')
                })
                // Mailbox
                // ----------------------------------- 
                .state('app.mailbox', {
                    url: '/mailbox/:folder',
                    templateUrl: basepath('mailbox.html'),
                    resolve: requireDeps('moment')
                })
                .state('app.mailbox.view', {
                    url: '/:id',
                    views: {
                        'mails@app.mailbox': {
                            templateUrl: basepath('mailbox-view-mail.html')
                        }
                    }
                })
                .state('app.mailbox.compose', {
                    url: '/compose',
                    views: {
                        'mails@app.mailbox': {
                            templateUrl: basepath('mailbox-compose.html')
                        }
                    }
                })
            // 
            // CUSTOM RESOLVE FUNCTION
            //   Add your own resolve properties
            //   following this object extend
            //   method
            // ----------------------------------- 
            // .state('app.yourRouteState', {
            //   url: '/route_url',
            //   templateUrl: 'your_template.html',
            //   controller: 'yourController',
            //   resolve: angular.extend(
            //     requireDeps(...), {
            //     // YOUR CUSTOM RESOLVES HERE
            //     }
            //   )
            // })
            ;


        // Change here your views base path
        function basepath(uri) {
            return 'app/html/' + uri;
        }

        // Generates a resolve object by passing script names
        // previously configured in constant.appDependencies
        // Also accept functions that returns a promise
        function requireDeps() {
            var _args = arguments;
            return {
                deps: [
                    '$ocLazyLoad', '$q', function($ocLL, $q) {
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
                                return promise.then(function() {
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
    '$tooltipProvider', function($tooltipProvider) {
        $tooltipProvider.options({ appendToBody: true });
    }
]).config([
    'cfpLoadingBarProvider', function(cfpLoadingBarProvider) {
        cfpLoadingBarProvider.includeBar = true;
        cfpLoadingBarProvider.includeSpinner = false;
        cfpLoadingBarProvider.latencyThreshold = 500;
        cfpLoadingBarProvider.parentSelector = '.app-container > section';
    }
]);