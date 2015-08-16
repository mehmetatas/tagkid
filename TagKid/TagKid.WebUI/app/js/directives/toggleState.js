
app.directive("toggleState", ["toggleStateService", function (toggle) {
    "use strict";

    return {
        restrict: "A",
        link: function (scope, element, attrs) {

            var $body = angular.element("body");

            $(element)
              .on("click", function (e) {
                  e.preventDefault();
                  var classname = attrs.toggleState;

                  if (classname) {
                      if ($body.hasClass(classname)) {
                          $body.removeClass(classname);
                          if (!attrs.noPersist)
                              toggle.removeState(classname);
                      }
                      else {
                          $body.addClass(classname);
                          if (!attrs.noPersist)
                              toggle.addState(classname);
                      }

                  }

              });
        }
    };

}]);