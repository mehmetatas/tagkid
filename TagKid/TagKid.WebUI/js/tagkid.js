;
tagkid = {
    context: {
        client: null,
        $state: null
    },
    user: function (u) {
        return tagkid.cookies.user(u);
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
        _cookie: function (name, value, expireDays) {
            $.cookie.json = true;
            if (typeof (value) === 'undefined') {
                return $.cookie(name);
            } else {
                if (value === null) {
                    $.removeCookie(name, {
                        path: '/'
                    });
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
            return tagkid.cookies._cookie('authToken', value);
        },
        authTokenId: function (value) {
            return tagkid.cookies._cookie('authTokenId', value);
        },
        user: function (value) {
            return tagkid.cookies._cookie('user', value);
        }
    },
    redirectIfLoggedIn: function () {
        var req = {
            token: tagkid.cookies.authToken(),
            tokenId: tagkid.cookies.authTokenId()
        };

        if (req.token && req.tokenId) {
            if (tagkid.user()) {
                tagkid.go('pages.timeline');
            } else {
                tagkid.auth.signInWithToken(req);
            }
        }
    },
    ensureLoggedIn: function () {
        var req = {
            token: tagkid.cookies.authToken(),
            tokenId: tagkid.cookies.authTokenId()
        };

        if (req.token && req.tokenId) {
            if (!tagkid.user()) {
                tagkid.auth.signInWithToken(req);
            }
            return;
        }

        tagkid.go('auth.signin');
    },
    auth: {
        signUpWithEmail: function (req, success, error) {
            tagkid.post('auth/signUpWithEmail', req, success, error);
        },
        signInWithPassword: function (req) {
            tagkid.post('auth/signInWithPassword', req,
                function (resp) {
                    tagkid.user({
                        Username: resp.Username,
                        Fullname: resp.Fullname,
                        ProfileImageUrl: resp.ProfileImageUrl,
                    });
                    tagkid.go('pages.timeline');
                });
        },
        signInWithToken: function (req) {
            tagkid.post('auth/signInWithToken', req,
                function (resp) {
                    tagkid.user({
                        Username: resp.Username,
                        Fullname: resp.Fullname,
                        ProfileImageUrl: resp.ProfileImageUrl,
                    });
                },
                function () {
                    tagkid.user(null);
                    tagkid.cookies.authToken(null);
                    tagkid.cookies.authTokenId(null);
                    tagkid.go('auth.signin');
                });
        },
        signOut: function () {
            tagkid.post('auth/signOut');

            tagkid.user(null);
            tagkid.cookies.authToken(null);
            tagkid.cookies.authTokenId(null);
            tagkid.go('auth.signin');
        },
        activateAccount: function (ccid, cc, onSuccess, onFail) {
            tagkid.get('auth/activateAccount', {
                ConfirmationCodeId: ccid,
                ConfirmationCode: cc
            },
                function (resp) {
                    tagkid.user({
                        Username: resp.Username,
                        Fullname: resp.Fullname,
                        ProfileImageUrl: resp.ProfileImageUrl,
                    });
                    tagkid.go('pages.timeline');
                },
                function (resp) {
                    tagkid.user(null);
                    tagkid.cookies.authToken(null);
                    tagkid.cookies.authTokenId(null);
                    onFail(resp);
                });
        }
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
        })
            .success(function (resp, status, headers, config) {
                var authToken = headers('tagkid-auth-token');
                var authTokenId = headers('tagkid-auth-token-id');

                if (authToken && authTokenId) {
                    tagkid.cookies.authToken(authToken);
                    tagkid.cookies.authTokenId(authTokenId);
                }

                if (resp.ResponseCode == 0) {
                    if (onSuccess) {
                        onSuccess(resp, headers);
                    }
                } else {
                    var isSecurityError = resp.ResponseCode >= 100 && resp.ResponseCode < 200;

                    if (onError) {
                        onError(resp, headers);
                    } else {
                        alert(resp.ResponseMessage);
                    }

                    if (isSecurityError) {
                        tagkid.user(null);
                        tagkid.cookies.authToken(null);
                        tagkid.cookies.authTokenId(null);
                    }
                }
            })
            .error(function (data, status, headers, config) {
                alert('Ooops! Something terribly went wrong :(');
            });
    };
};