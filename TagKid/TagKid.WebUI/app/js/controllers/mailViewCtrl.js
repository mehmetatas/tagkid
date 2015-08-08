app.controller('mailViewCtrl', ["$scope", "$stateParams", function ($scope, $stateParams) {

    // move the current viewing mail data to the inner view scope
    $scope.viewMail = $scope.mailList[$stateParams.id];

}]);