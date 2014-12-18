app.controller('ActivationCtrl', [
    '$scope', '$stateParams', 'tagkid', 'auth', function ($scope, $stateParams, tagkid, auth) {
        if ($stateParams.ccid && $stateParams.cc) {
            auth.activateAccount($stateParams.ccid, $stateParams.cc, function() {
                
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