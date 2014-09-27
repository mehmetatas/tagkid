
tagkidApp.controller('dashboardController', function ($scope, $http, $location, authService) {
    tagkid.setContext($scope, $http, $location, authService);
    $scope.pageClass = 'dashboard';

    $scope.onSignOutClick = function () {
        tagkid.signout();
    };

    tagkid.ensureLoggedIn();

    tgEditor.init('.tg-input', '.tg-preview');

    $('.editor-tab-buttons input').on('change', function (e) {
        e.preventDefault();

        $('.editor-tab-buttons input').each(function() {
            var tabId = $(this).attr('data-tab');
            $(tabId).hide();
        });

        var tabId = $(this).attr('data-tab');
        $(tabId).show();
    });
});