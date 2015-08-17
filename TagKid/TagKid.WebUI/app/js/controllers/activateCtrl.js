app.controller("activateCtrl", [
    "$scope", "$stateParams", "$state", "err", "auth", "dialogService", function ($scope, $stateParams, $state, err, auth, dialogService) {
        auth.activateRegistration({
            Id: $stateParams.id,
            Token: $stateParams.token
        }, function() {
            dialogService.openAuthDialog(function() {
                $state.go("app.mailbox");
            });
        }, function(err) {
            alert(err.ResponseMessage);
        });
    }
]);