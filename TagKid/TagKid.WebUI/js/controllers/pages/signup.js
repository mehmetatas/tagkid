app.controller('SignUpCtrl', [
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

        $scope.openTermsAndPolicy = function () {
            $modal.open({
                templateUrl: 'termsAndPolicyModalContent.html',
                controller: 'TermsAndPolicyModalCtrl'
            }).result.then(function (newCategory) {
                $scope.categories.push(newCategory);
                $scope.selectedCategory = newCategory;
            });
        };
    }
]);