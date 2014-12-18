app.controller('TermsAndPolicyModalCtrl', [
    '$scope', '$modalInstance', function ($scope, $modalInstance) {
        $scope.close = function () {
            $modalInstance.close();
        };
    }
]);
