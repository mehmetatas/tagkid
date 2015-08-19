app.service("validation", function () {
    var isNullOrUndefined = function (obj) {
        return typeof obj === "undefined" || obj === null;
    };

    var strIsNullOrEmpty = function (str) {
        return isNullOrUndefined(str) || str.length === 0;
    };

    var strIsNullOrWhiteSpace = function (str) {
        if (strIsNullOrEmpty(str)) {
            return true;
        }
        str = str.trim();
        return str.length === 0;
    };

    this.executeCharsetRule = function (str, charset, caseSensitive) {
        if (strIsNullOrEmpty(str)) {
            return false;
        }

        if (!caseSensitive) {
            str = str.toLowerCase();
        }

        for (var i = 0; i < str.length; i++) {
            if (charset.indexOf(str[i]) < 0) {
                return false;
            }
        }

        return true;
    };

    this.executeEqualsRule = function (obj, expected) {
        return obj === expected;
    };

    this.executeGuidRule = function (str) {
        var regex = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;
        return regex.test(str);
    };

    this.executeInRule = function (obj, arr) {
        return arr.indexOf(obj) >= 0;
    };

    this.executeNullRule = function (obj) {
        return isNullOrUndefined(obj);
    };

    this.executeNotNullRule = function (obj) {
        return !isNullOrUndefined(obj);
    };

    this.executePasswordRule = function (str) {
        return !strIsNullOrWhiteSpace(str) && str.length >= 6 && str.length <= 32;
    };

    this.executeStringNotEmptyRule = function (str) {
        return !strIsNullOrWhiteSpace(str);
    };

    this.executeStringLengthRule = function (str, min, max) {
        if (isNullOrUndefined(str)) {
            return false;
        }

        str = str.trim();
        return min <= str.length && str.length <= max;
    };

    this.executeRegexRule = function(str, pattern) {
        var regex = new RegExp(pattern, "i");
        regex.test(str);
    };
});