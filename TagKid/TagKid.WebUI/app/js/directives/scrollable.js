app.directive('scrollable', function () {
    'use strict';
    return {
        restrict: 'EA',
        link: function (scope, elem, attrs) {
            var defaultHeight = 285;

            attrs.height = attrs.height || defaultHeight;

            elem.slimScroll(attrs);

        }
    };
});