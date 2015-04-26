var authDialogCtrl = function ($scope, $modalInstance) {
    $scope.close = function () {
        $modalInstance.close();
    };

    $scope.signupReq = {};
    $scope.signinReq = {};
    $scope.forgotPwdReq = {};

    $scope.toSignin = function (fromSignup) {
        $scope.mode = 1;

        if (fromSignup) {
            if ($scope.signupReq.Email) {
                $scope.signinReq.EmailOrUsername = $scope.signupReq.Email;
            }
            else if ($scope.signupReq.Username) {
                $scope.signinReq.EmailOrUsername = $scope.signupReq.Username;
            }

            if ($scope.signupReq.Password) {
                $scope.signinReq.Password = $scope.signupReq.Password;
            }
        } else {
            if ($scope.forgotPwdReq.Email) {
                $scope.signinReq.EmailOrUsername = $scope.forgotPwdReq.Email;
            }
        }
    };

    $scope.toSignup = function () {
        $scope.mode = 0;

        if ($scope.signinReq.EmailOrUsername) {
            if ($scope.signinReq.EmailOrUsername.indexOf('@') > 0) {
                $scope.signupReq.Email = $scope.signinReq.EmailOrUsername;
            } else {
                $scope.signupReq.Username = $scope.signinReq.EmailOrUsername;
            }
        }
        if ($scope.signinReq.Password) {
            $scope.signupReq.Password = $scope.signinReq.Password;
        }
    };

    $scope.toForgotPwd = function () {
        $scope.mode = 2;

        if ($scope.signinReq.EmailOrUsername && $scope.signinReq.EmailOrUsername.indexOf('@') > 0) {
            $scope.forgotPwdReq.Email = $scope.signinReq.EmailOrUsername;
        }
    };

    $scope.sendNewPassword = function () {
        alert('TODO: Send New Password');
        $scope.passwordSent = true;
    };

    $scope.signin = function () {
        alert('signin');
    };

    $scope.signup = function () {
        alert('signup');
    };

    $scope.openTermsAndPolicy = function () {
        alert('open terms and policy');
    };
};

angular.module('app').controller('authDialogCtrl', ['$scope', '$modalInstance', authDialogCtrl]);