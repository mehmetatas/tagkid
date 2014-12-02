app.controller('TimelineCtrl', ['$scope', '$http', function($scope, $http) {
    $scope.Tags = [
       { Name: 'c#', Hint: 'c-sharp', Description: 'programming language' },
       { Name: 'java', Hint: 'java', Description: 'open source programming language' },
       { Name: 'javascript', Hint: 'jscript', Description: 'scripting language' },
       { Name: 'php', Hint: 'php Hint', Description: 'php desc' },
       { Name: 'sql-server', Hint: 'microsoft', Description: 'ms sql server desc' },
       { Name: 'oracle', Hint: '12g', Description: 'oracle desc' },
       { Name: 'mysql', Hint: 'db', Description: 'mysql desc' },
       { Name: 'phyton', Hint: 'snake', Description: 'piton desc' },
       { Name: 'ruby-on-rails', Hint: 'ruby', Description: 'ruby on rails desc' },
       { Name: 'objective-c', Hint: 'ios', Description: 'objective c desc' }
    ];
}]);

app.filter('propsFilter', function() {
    return function(items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function(item) {
                var itemMatches = false;

                var keys = Object.keys(props);
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});