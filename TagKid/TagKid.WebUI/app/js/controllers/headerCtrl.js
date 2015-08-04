app.controller('headerCtrl', ['$scope', '$rootScope', function ($scope, $rootScope) {
    'use strict';
    $scope.app = $rootScope.app;

    $scope.headerMenuCollapsed = true;

    $scope.toggleHeaderMenu = function () {
        $scope.headerMenuCollapsed = !$scope.headerMenuCollapsed;
    };
}]);