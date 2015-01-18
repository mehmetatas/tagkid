app.service('tagkid', [
    '$http', '$state', function ($http, $state) {
        var cookies = {
            authToken: function (value) {
                return $cookie('authToken', value);
            },
            authTokenId: function (value) {
                return $cookie('authTokenId', value);
            },
            user: function (value) {
                return $cookie('user', value);
            }
        };

        var $cookie = function (name, value, expireDays) {
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
        };
        
        var $send = function (method, url, data, success, error, complete) {
            var opts = {
                method: method,
                url: url,
                headers: {
                    'tagkid-auth-token': cookies.authToken(),
                    'tagkid-auth-token-id': cookies.authTokenId()
                }
            };

            if (method === 'GET') {
                opts.params = data;
            } else {
                opts.data = data;
            }

            $('.butterbar').removeClass('hide').addClass('active');

            $http(opts)
                .success(function (resp, status, headers, config) {
                    var authToken = headers('tagkid-auth-token');
                    var authTokenId = headers('tagkid-auth-token-id');

                    if (authToken && authTokenId) {
                        cookies.authToken(authToken);
                        cookies.authTokenId(authTokenId);
                    }

                    if (resp.ResponseCode == 0) {
                        if (success) {
                            success(resp, headers);
                        }
                    } else {
                        var isSecurityError = resp.ResponseCode >= 100 && resp.ResponseCode < 200;

                        if (error) {
                            error(resp, headers);
                        } else {
                            alert(resp.ResponseMessage);
                        }

                        if (isSecurityError) {
                            cookies.user(null);
                            cookies.authToken(null);
                            cookies.authTokenId(null);
                        }
                    }
                })
                .error(function (resp, status, headers, config) {
                    alert('Ooops! Something terribly went wrong :(');
                })
                .finally(function () {
                    $('.butterbar').removeClass('active').addClass('hide');
                    if (complete) {
                        complete();
                    }
                });
        };

        return {
            go: $state.go,
            user: cookies.user,
            cookies: cookies,
            post: function (controller, action, data, succes, error, complete) {
                var url = '/api/' + controller + '/' + action;
                return $send('POST', url, data, succes, error, complete);
            },
            get: function (controller, action, data, succes, error, complete) {
                var url = '/api/' + controller + '/' + action;
                return $send('GET', url, data, succes, error, complete);
            }
        };
    }
]);