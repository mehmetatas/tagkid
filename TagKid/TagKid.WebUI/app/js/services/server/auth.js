app.service("auth", ["tagkid",
    function (tagkid) {
        "use strict";

        this.register = function(reuqest, success, error, complete) {
            tagkid.post("auth", "register", reuqest, success, error, complete);
        };

        this.activateRegistration = function (reuqest, success, error, complete) {
            tagkid.post("auth", "activateRegistration", reuqest, success, error, complete);
        };

        this.loginWithPassword = function (reuqest, success, error, complete) {
            tagkid.post("auth", "loginWithPassword", reuqest, success, error, complete);
        };
    }
]);