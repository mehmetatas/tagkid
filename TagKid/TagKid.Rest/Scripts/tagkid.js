var tagkid = {
    apiUrl: 'http://localhost:53495/api/',
    cookie: {
        authToken: function (token) {
            if (token) {
                $.cookie('tagkid.authToken.token', token, { path: '/' });
            } else {
                return $.cookie('tagkid.authToken.token');
            }
            return null;
        },
        authTokenId: function (id) {
            if (id) {
                $.cookie('tagkid.authToken.id', id, { path: '/' });
            } else {
                return $.cookie('tagkid.authToken.id');
            }
            return null;
        },
    },
    client: {
        ajax: function (type, route, requestObj, onSuccess, onError) {
            $.ajax(tagkid.apiUrl + route, {
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(requestObj),
                dataType: 'json',
                type: type,
                headers: {
                    'tagkid-auth-token': tagkid.cookie.authToken(),
                    'tagkid-auth-token-id': tagkid.cookie.authTokenId(),
                },
                success: function(response, textStatus, jqXhr) {
                    onSuccess();
                },
                error: function(jqXhr, textStatus, errorThrown) {
                    onError();
                }
            });
        }
    },
    authService: {
        signup: function (signupRequest) {
            tagkid.client.ajax('POST', 'auth/signup', signupRequest,
                function() {
                     alert('success');
                },
                function() {
                     alert('error');
                });
        }
    }
};