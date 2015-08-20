app.service("dialogService", ["$modal", function ($modal) {
    this.openAuthDialog = function (callback) {
        $modal.open({
            templateUrl: "/app/html/dialogs/auth.html",
            controller: "authDialogCtrl",
            size: "sm"
        }).result.then(callback);
    };
}]);