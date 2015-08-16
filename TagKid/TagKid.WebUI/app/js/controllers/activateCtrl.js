app.controller("activateCtrl", [
    "$scope", "$stateParams", "$state", "$timeout", function ($scope, $stateParams, $state, $timeout) {
        $scope.token = $stateParams.token;
        $timeout(function() {
            $state.go("app.mailbox");
        },2000);
    }
]);