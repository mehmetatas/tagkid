var tagkid = {
    apiUrl: 'http://localhost:53495/api/',
    constants: {
        apiVersion: 'tagkid-api-version',
        authToken: 'tagkid-auth-token',
        authTokenId: 'tagkid-auth-token-id'
    },
    cookie: {
        authToken: function () {
            return $.cookie(tagkid.constants.authToken);
        },
        authTokenId: function () {
            return $.cookie(tagkid.constants.authTokenId);
        },
        setAuthToken: function (jqXhr) {
            var token = jqXhr.getResponseHeader(tagkid.constants.authToken);
            var tokenId = jqXhr.getResponseHeader(tagkid.constants.authTokenId);

            if (token && tokenId) {
                $.cookie(tagkid.constants.authToken, token, { path: '/', expires: 15 });
                $.cookie(tagkid.constants.authTokenId, tokenId, { path: '/', expires: 15 });
            }
        },
        deleteAuthToken: function () {
            $.removeCookie(tagkid.constants.authToken, { path: '/' });
            $.removeCookie(tagkid.constants.authTokenId, { path: '/' });
        }
    },
    client: {
        ajax: function (type, route, requestObj, onSuccess, onError) {
            var headers = {};
            headers[tagkid.constants.apiVersion] = 'v0';
            headers[tagkid.constants.authToken] = tagkid.cookie.authToken();
            headers[tagkid.constants.authTokenId] = tagkid.cookie.authTokenId();

            $.ajax(tagkid.apiUrl + route, {
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(requestObj),
                dataType: 'json',
                type: type,
                headers: headers,
                success: function (resp, textStatus, jqXhr) {
                    tagkid.cookie.setAuthToken(jqXhr);
                    if (resp.ResponseCode == 0) {
                        onSuccess(resp);
                    } else {
                        onError(resp, textStatus, jqXhr);
                    }
                },
                error: function (jqXhr, textStatus, errorThrown) {
                    onError({
                        ResponseCode: -1,
                        ResponseMessage: 'Unknown error!'
                    }, jqXhr, textStatus, errorThrown);
                }
            });
        }
    },
    authService: {
        signUpWithEmail: function (signUpWithEmailRequest) {
            tagkid.client.ajax('POST', 'auth/signUpWithEmail', signUpWithEmailRequest,
                function (resp) {
                    alert(resp.ResponseCode + ' : ' + resp.ResponseMessage);
                },
                function (resp) {
                    alert(resp.ResponseCode + ' : ' + resp.ResponseMessage);
                });
        },
        signInWithPassword: function (signInWithPasswordRequest) {
            tagkid.client.ajax('POST', 'auth/signInWithPassword', signInWithPasswordRequest,
                function (resp) {
                    alert(resp.ResponseCode + ' : ' + resp.ResponseMessage);
                },
                function (resp) {
                    alert(resp.ResponseCode + ' : ' + resp.ResponseMessage);
                });
        }
    }
};