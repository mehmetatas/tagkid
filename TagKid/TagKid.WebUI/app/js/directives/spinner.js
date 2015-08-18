app.directive("spinner", function () {
    "use strict";
    return {
        scope: { size: "=" },
        template: "<img src='/app/img/spinner.gif' ng-style='{\"width\": size + \"px\", \"height\": size + \"px\"}' />",
        restrict: "E",
        replace: true,
        link: function (scope, elem, attrs) {
            if (!attrs.size)
                scope.size = 32;
        }
    };
});