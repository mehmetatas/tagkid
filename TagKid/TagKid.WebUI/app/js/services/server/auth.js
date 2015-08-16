app.service("auth", ["tagkid",
    function (tagkid) {
        "use strict";

        this.register = function(reuqest, success, error, complete) {
            tagkid.post("auth", "register", reuqest, success, error, complete);
        };
    }
]);