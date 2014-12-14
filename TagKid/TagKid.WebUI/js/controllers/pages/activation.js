app.controller('ActivationCtrl', [
    '$scope', '$stateParams', function ($scope, $stateParams) {
        if ($stateParams.ccid && $stateParams.cc) {
            tagkid.auth.activateAccount($stateParams.ccid, $stateParams.cc, function() {
                
            }, function (resp) {
                $scope.error = resp.ResponseMessage;
            });
        } else {
            tagkid.go('auth.signin');
        }

        $scope.requestNewActivationCode = function() {
            alert('request new activation code!');
        };
    }
]);