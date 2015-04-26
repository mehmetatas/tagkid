angular.module('app').service('authDialog', ['$modal', function($modal) {
    this.show = function(callback) {
        $modal.open({
            templateUrl: '/Dialogs/Auth',
            controller: 'authDialogCtrl'
        }).result.then(callback);
    };
}]);