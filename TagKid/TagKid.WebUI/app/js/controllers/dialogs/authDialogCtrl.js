app.controller("authDialogCtrl", ["$scope", "$modalInstance", "auth", function ($scope, $modalInstance, auth) {
    function isValidEmail(email) {
        var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
        return re.test(email);
    }

    $scope.reset = function() {
        $scope.registerReq = {};
        $scope.loginReq = {};
        $scope.recoverReq = {};

        $scope.mode = 1;

        $scope.registrationMode = 0;
        $scope.registrationError = null;
    };

    $scope.reset();

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
        $scope.registrationError = null;
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
        $scope.registrationError = null;
        $scope.registrationMode = 1;

        auth.register($scope.registerReq, function () {
            $scope.registrationMode = 2;
        }, function(resp) {
            $scope.registrationMode = 0;
            $scope.registrationError = resp.ResponseMessage;
        });
    };
    
    $scope.connectWith = function () {
        alert("show connect options");
    };

    $scope.recoverPassword = function () {
        alert("recover password: " + JSON.stringify($scope.recoverReq));
    };

    $scope.openTerms = function () {
        alert("open terms and policy");
    };
}]);