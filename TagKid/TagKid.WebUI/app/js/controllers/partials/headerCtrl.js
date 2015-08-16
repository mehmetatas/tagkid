app.controller("headerCtrl", ["$scope", "$rootScope", "dialogService", function ($scope, $rootScope, dialogService) {
    "use strict";
    $scope.app = $rootScope.app;
    $scope.user = $rootScope.user;

    $scope.headerMenuCollapsed = true;

    $scope.toggleHeaderMenu = function () {
        $scope.headerMenuCollapsed = !$scope.headerMenuCollapsed;
    };

    $scope.auth = function () {
        dialogService.openAuthDialog(function () {
            alert("ok!");
        });
    };
}]);