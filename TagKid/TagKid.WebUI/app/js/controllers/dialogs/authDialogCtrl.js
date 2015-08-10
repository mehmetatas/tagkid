app.controller('authDialogCtrl', ['$scope', '$modalInstance', function ($scope, $modalInstance) {
    $scope.registerReq = {};
    $scope.loginReq = {};
    $scope.recoverReq = {};

    $scope.toLogin = function (fromRegistration) {
        $scope.mode = 0;

        if (fromRegistration) {
            if ($scope.registerReq.Email) {
                $scope.loginReq.EmailOrUsername = $scope.registerReq.Email;
            }
            else if ($scope.registerReq.Username) {
                $scope.loginReq.EmailOrUsername = $scope.registerReq.Username;
            }

            if ($scope.registerReq.Password) {
                $scope.loginReq.Password = $scope.registerReq.Password;
            }
        } else {
            if ($scope.recoverReq.Email) {
                $scope.loginReq.EmailOrUsername = $scope.recoverReq.Email;
            }
        }
    };

    $scope.toRegistration = function () {
        $scope.mode = 1;

        if ($scope.loginReq.EmailOrUsername) {
            if ($scope.loginReq.EmailOrUsername.indexOf('@') > 0) {
                $scope.registerReq.Email = $scope.loginReq.EmailOrUsername;
            } else {
                $scope.registerReq.Username = $scope.loginReq.EmailOrUsername;
            }
        }
        if ($scope.loginReq.Password) {
            $scope.registerReq.Password = $scope.loginReq.Password;
        }
    };

    $scope.toRecover = function () {
        $scope.mode = 2;

        if ($scope.loginReq.EmailOrUsername && $scope.loginReq.EmailOrUsername.indexOf('@') > 0) {
            $scope.recoverReq.Email = $scope.loginReq.EmailOrUsername;
        }
    };

    $scope.login = function () {
        alert('login: ' + JSON.stringify($scope.loginReq));
    };

    $scope.register = function () {
        alert('register: ' + JSON.stringify($scope.registerReq));
    };

    $scope.recoverPassword = function () {
        alert('recover password: ' + JSON.stringify($scope.recoverReq));
    };

    $scope.openTerms = function () {
        alert('open terms and policy');
    };
}]);