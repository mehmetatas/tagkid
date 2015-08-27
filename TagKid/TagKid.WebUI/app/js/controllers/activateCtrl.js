app.controller("activateCtrl", [
    "$scope", "$stateParams", "$state", "auth", function ($scope, $stateParams, $state, auth) {
        $scope.activating = true;

        auth.activateRegistration({
            Id: $stateParams.id,
            Token: $stateParams.token
        }, function() {
            $state.go("app.mailbox");
        }, function(err) {
            $scope.error = err.ResponseMessage;
        }, function() {
            $scope.activating = false;
        });
    }
]);