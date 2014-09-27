tagkid = {
    context: {
        $scope: null,
        $http: null,
        $location: null,
        authService: null
    },
    setContext: function ($scope, $http, $location, authService) {
        tagkid.context.$scope = $scope;
        tagkid.context.$http = $http;
        tagkid.context.$location = $location;
        tagkid.context.authService = authService;
    },
    location: function (path) {
        tagkid.context.$location.path(path);
    },
    post: function(url, data) {
        return tagkid.context.$http.post(url, data);
    },
    cookies: {
        cookie: function (name, value, expireDays) {
            if (typeof (value) === 'undefined') {
                return $.cookie(name);
            } else {
                if (value === null) {
                    $.removeCookie(name);
                } else {
                    $.cookie(name, value, {
                        path: '/',
                        expires: expireDays ? expireDays : 7
                    });
                }
                return null;
            }
        },
        authToken: function (value) {
            return tagkid.cookies.cookie('authToken', value);
        },
        authTokenId: function (value) {
            return tagkid.cookies.cookie('authTokenId', value);
        },
        requestToken: function (value) {
            return tagkid.cookies.cookie('requestToken', value, 1);
        }
    },
    redirectToDashboard: function (resp) {
        tagkid.cookies.authToken(resp.authToken);
        tagkid.cookies.authTokenId(resp.authTokenId);
        tagkid.cookies.requestToken(resp.requestToken);

        tagkid.context.authService.user = resp.user;
        tagkid.context.$scope.user = resp.user;
        tagkid.location("/dashboard");
    },
    redirectToDashboardIfLoggedIn: function () {
        if (tagkid.cookies.authToken() && tagkid.cookies.authTokenId()) {
            tagkid.post('/api/auth/validate_auth_cookie')
            .success(function (resp) {
                if (resp.responseCode == 0) {
                    tagkid.redirectToDashboard(resp);
                } else {
                    tagkid.location("/");
                }
            }).error(function (err) {
                tagkid.location("/");
            });
        }
    },
    ensureLoggedIn: function () {
        if (!tagkid.cookies.authToken() || !tagkid.cookies.authTokenId()) {
            tagkid.location("/");
            return;
        }

        if (tagkid.context.authService.user) {
            tagkid.context.$scope.user = tagkid.context.authService.user;
        } else {
            tagkid.redirectToDashboardIfLoggedIn();
        }
    },
    signout: function () {
        tagkid.cookies.authToken(null);
        tagkid.cookies.authTokenId(null);
        tagkid.cookies.requestToken(null);
        tagkid.context.authService.user = null;
        tagkid.context.$scope.user = null;
        tagkid.location("/");
    }
};

$(window).load(function () {
    // Page Preloader
    $('#status').fadeOut();
    $('#preloader').delay(350).fadeOut(function () {
        $('body').delay(350).css({ 'overflow': 'visible' });
    });
});