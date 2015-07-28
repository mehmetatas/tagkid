angular.module('app')
    .directive('tagkidEditor', ['postService', function (postService) {
        var $scope;

        var save = function (accessLevel) {
            postService.save({ Post: $scope.options.post });
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

            $scope = scope;
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