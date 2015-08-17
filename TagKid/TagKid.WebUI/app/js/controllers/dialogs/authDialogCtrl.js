app.controller("authDialogCtrl", ["$scope", "$modalInstance", "auth", function ($scope, $modalInstance, auth) {
    $scope.registerReq = {};
    $scope.loginReq = {};
    $scope.recoverReq = {};

    $scope.mode = 1;

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
            if ($scope.loginReq.EmailOrUsername.indexOf("@") > 0) {
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

        if ($scope.loginReq.EmailOrUsername && $scope.loginReq.EmailOrUsername.indexOf("@") > 0) {
            $scope.recoverReq.Email = $scope.loginReq.EmailOrUsername;
        }
    };

    $scope.login = function () {
        auth.loginWithPassword($scope.loginReq, function() {
            alert('logged in');
        });
    };

    $scope.register = function () {
        auth.register($scope.registerReq, function () {
            alert("registered!");
        });
    };

    $scope.recoverPassword = function () {
        alert("recover password: " + JSON.stringify($scope.recoverReq));
    };

    $scope.openTerms = function () {
        alert("open terms and policy");
    };
}]);