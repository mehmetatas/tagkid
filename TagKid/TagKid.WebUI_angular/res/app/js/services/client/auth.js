angular.module('app').service('auth', [
    function () {
        var auth = {};

        var prop = function (key, value) {
            if (value) {
                auth[key] = value;
            } else {
                return auth[key];
            }
        };

        this.user = function (u) {
            return prop('user', u);
        };

        this.token = function (t) {
            return prop('token', t);
        };
    }
]);