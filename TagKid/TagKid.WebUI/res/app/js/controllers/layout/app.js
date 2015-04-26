
angular.module('app').controller('appCtrl', ['$scope', '$window', function ($scope, $window) {

    function isSmartDevice() {
        // Adapted from http://www.detectmobilebrowsers.com
        var ua = $window['navigator']['userAgent'] || $window['navigator']['vendor'] || $window['opera'];
        // Checks for iOs, Android, Blackberry, Opera Mini, and Windows mobile devices
        return (/iPhone|iPod|iPad|Silk|Android|BlackBerry|Opera Mini|IEMobile/).test(ua);
    }

    // add 'ie' classes to html
    var isie = !!navigator.userAgent.match(/MSIE/i);
    isie && angular.element($window.document.body).addClass('ie');
    isSmartDevice() && angular.element($window.document.body).addClass('smart');

    $scope.settings = {
        asideFolded: false
    };
    $scope.$watch('app.settings', function () {
        // Todo: save settings
    }, true);
}]);