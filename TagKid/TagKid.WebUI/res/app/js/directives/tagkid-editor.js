angular.module('app')
    .directive('tagkidEditor', [function () {
        var save = function (accessLevel) {
            alert('save');
        };

        var clear = function (accessLevel) {
            alert('clear');
        };

        var showSignupDialog = function () {
            alert();
        };

        var linker = function (scope, element, attrs) {
            scope.save = save;
            scope.clear = clear;
            scope.showSignupDialog = showSignupDialog;

            scope.user = {
                Username: 'mehmetatas',
                Fullname: 'Mehmet Ataş',
                ProfileImageUrl: '/res/app/img/a0.jpg'
            };

            scope.edit = function (p) {
                scope.onEdit({ post: p });
            };
        };

        return {
            restrict: 'E',
            link: linker,
            scope: {
                options: '='
            },
            templateUrl: '/Directives/Editor'
        };
    }]);