﻿app.controller('SignUpCtrl', [
    '$scope', '$modal', 'auth', function ($scope, $modal, auth) {
        $scope.isCollapsed = true;

        $scope.signup = function() {
            auth.signUpWithEmail($scope.req,
                function (resp, header) {
                    $scope.isCollapsed = false;
                },
                function (resp) {
                     $scope.authError = resp.ResponseMessage;
                });
        };

        $scope.signUpAnonymous = function () {
            auth.signUpAnonymous();
        };

        $scope.openTermsAndPolicy = function () {
            $modal.open({
                templateUrl: 'termsAndPolicyModalContent.html',
                controller: 'TermsAndPolicyModalCtrl'
            });
        };
    }
]);