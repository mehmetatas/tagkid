app.service("translate", [function () {
    var cache = {};

    this.load = function (group) {
        if (cache[group]) {
            return;
        }
        // TODO: load group
    };

    this.get = function (key, params) {
        for (var group in cache) {
            if (cache[group][key]) {
                key = cache[group][key];
                // TODO: string.format(key, params)
                break;
            }
        }

        return key;
    };
}]);