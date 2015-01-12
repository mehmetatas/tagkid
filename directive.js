ermsModule.directive('ngEriAutocomplete', [function () {
    return {
        restrict: 'A',
        scope: {
            onSearch: '&',
            onOpenDetailSearch: '&',
            delay: '=',
            textField: '=',
            filterField: '=',
            minFilterLength: '='
        },
        controller: [
            '$scope', '$timeout', function ($scope, $timeout) {
                $scope.filter = '';
                $scope.showLoading = false;
                $scope.showResults = false;
                $scope.showResults = false;
                $scope.searchResults = [];

                var searchTimeout;
                var doSearch = function () {
                    $scope.searchResults = $scope.onSearch({ filter: $scope.filter });
                    $scope.showLoading = false;
                    $scope.showResults = true;
                };

                $scope.onInputBlur = function () {
                    $scope.showPanel = false;
                    $scope.showLoading = false;
                    $scope.showResults = false;
                };

                $scope.onInputFocus = function () {
                    if ($scope.$item) {
                        $scope.filter = $scope.$item[$scope.filterField];
                    }
                };

                $scope.search = function () {
                    $scope.showResults = false;
                    $scope.$item = null;

                    var minLength = $scope.minFilterLength || 2;
                    if ($scope.filter.length < minLength) {
                        $scope.showPanel = false;
                        $scope.showLoading = false;
                        return;
                    }

                    $scope.showPanel = true;
                    $scope.showLoading = true;

                    if (searchTimeout) {
                        $timeout.cancel(searchTimeout);
                    }
                    searchTimeout = $timeout(doSearch, $scope.delay || 300);
                };

                $scope.openDetailSearch = function () {
                    $scope.onOpenDetailSearch({ filter: $scope.filter });
                };

                $scope.selectItem = function (item) {
                    $scope.$item = item;
                    $scope.filter = $scope.$eval($scope.textField);
                };
            }
        ],
        template: function (element, attrs) {
            var htmlText = '<div id="productRequest-new-productSearchInput" class="ebInputWrapper eaERMS-actionBar-inputWrapper">' +
                '<input type="text" class="ebInput eaERMS-actionBar-input" placeholder="Enter Product ID or Product Name to add a product" ng-keyup="search();" ng-blur="onInputBlur();" ng-model="filter" ng-focus="onInputFocus();" />' +
                '<div id="productRequest-new-productSearchInput-results" class="ebInputWrapper-body eaERMS-actionBar-inputWrapper-body" ng-show="showPanel">' +
                '<ul class="ebComponentList eb_scrollbar" ng-show="showLoading">' +
                '<li class="ebComponentList-item">' +
                '<span class="eaERMS-productSearchItem">Loading...</span>' +
                '</li>' +
                '</ul>' +
                '<ul class="ebComponentList eb_scrollbar" ng-show="showResults">' +
                '<li class="ebComponentList-item" ng-repeat="$item in searchResults">' +
                '<a class="eaERMS-productSearchItem" ng-mousedown="selectItem($item);">' + element[0].innerHTML + '</a>' +
                '</li>';

            if (attrs.detailSearch !== 'false') {
                htmlText += '<li class="ebComponentList-separator" ng-hide="searchResults.length == 0"></li>' +
                    '<li class="ebComponentList-item eaERMS-componentList-viewMore" title="View More Results" ng-hide="searchResults.length == 0"><a href="" ng-click="openDetailSearch();">View More Results</a></li>';
            }

            htmlText += '<li class="ebText_error" ng-show="searchResults.length == 0">No product found!</li>' +
                '</ul>' +
                '</div>' +
                '</div>';

            return htmlText;
        }
    };
}]);
