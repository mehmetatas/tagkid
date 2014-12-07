
tagkid = {
    context: {
        user: null,
        client: null,
        $state: null
    },
    setNgContext: function ($http, $state) {
        tagkid.context.client = new NgClient($http);
        tagkid.context.$state = $state;
    },
    go: function (path) {
        tagkid.context.$state.go(path);
    },
    post: function (url, req, onSucces, onFail) {
        return tagkid.context.client.send('POST', '/api/' + url, req, onSucces, onFail);
    },
    get: function (url, req, onSucces, onFail) {
        var join = url.indexOf('?') >= 0 ? '&' : '?';
        for (prop in req) {
            url += join + prop + '=' + req[prop];
            join = '&';
        }
        return tagkid.context.client.send('GET', '/api/' + url, null, onSucces, onFail);
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
        }
    },
    redirectIfLoggedIn: function () {
        if (tagkid.cookies.authToken() && tagkid.cookies.authTokenId()) {
            // TODO: validate token
            tagkid.go('pages.timeline');
        }
    },
    ensureLoggedIn: function () {
        if (!tagkid.cookies.authToken() || !tagkid.cookies.authTokenId()) {
            tagkid.cookies.authToken(null);
            tagkid.cookies.authTokenId(null);
            tagkid.go('auth.signin');
        }
    },
    signout: function () {
        // TODO: call service
        tagkid.cookies.authToken(null);
        tagkid.cookies.authTokenId(null);
        tagkid.go('auth.signin');
    },
    auth: {
        signUpWithEmail: function (req, success, error) {
            tagkid.post('auth/signUpWithEmail', req, success, error);
        },
        signInWithPassword: function (req) {
            tagkid.post('auth/signInWithPassword', req, function (resp, header) {
                var authToken = header('tagkid-auth-token');
                var authTokenId = header('tagkid-auth-token-id');
                tagkid.cookies.authToken(authToken);
                tagkid.cookies.authTokenId(authTokenId);
                tagkid.go('pages.timeline');
            });
        },
    }
};

function NgClient($http) {
    this.send = function (method, url, req, onSuccess, onError) {
        $http({
            method: method,
            url: url,
            data: req,
            headers: {
                'tagkid-auth-token': tagkid.cookies.authToken(),
                'tagkid-auth-token-id': tagkid.cookies.authTokenId()
            }
        }).
        success(function (resp, status, headers, config) {
            if (!onSuccess) {
                return;
            }
            if (resp.ResponseCode == 0) {
                onSuccess(resp, headers);
            } else {
                if (onError) {
                    onError(resp, headers);
                } else {
                    alert(resp.ResponseMessage);
                }
            }
        }).
        error(function (data, status, headers, config) {
            if (!onError) {
                return;
            }
            alert('Ooops! Something terribly went wrong :(');
        });
    };
}