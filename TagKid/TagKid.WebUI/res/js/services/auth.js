app.factory('auth', [
    'tagkid', function (tagkid) {
        var signInWithToken = function(req) {
            tagkid.post('auth', 'signInWithToken', req,
                function(resp) {
                    tagkid.user({
                        Id: resp.Data.Id,
                        Username: resp.Data.Username,
                        Fullname: resp.Data.Fullname,
                        ProfileImageUrl: resp.Data.ProfileImageUrl
                    });
                },
                function () {
                    setAsAnonymous();
                });
        };

        var setAsAnonymous = function () {
            tagkid.user({
                IsAnonymous: true
            });
            tagkid.cookies.authToken(null);
            tagkid.cookies.authTokenId(null);
        };

        return {
            ensureLoggedIn: function () {
                var req = {
                    token: tagkid.cookies.authToken(),
                    tokenId: tagkid.cookies.authTokenId()
                };

                var user = tagkid.user();

                if (!user) {
                    if (req.token && req.tokenId) {
                        signInWithToken(req);
                    } else {
                        setAsAnonymous();
                    }
                }
            },
            signUpWithEmail: function(req, success, error) {
                tagkid.post('auth', 'signUpWithEmail', req, success, error);
            },
            signInWithPassword: function (req, success, error) {
                tagkid.post('auth', 'signInWithPassword', req,
                    function(resp) {
                        tagkid.user({
                            Id: resp.Data.Id,
                            Username: resp.Data.Username,
                            Fullname: resp.Data.Fullname,
                            ProfileImageUrl: resp.Data.ProfileImageUrl
                        });
                        if (success) {
                            success(resp);
                        }
                    }, function(resp) {
                        setAsAnonymous();
                        if (error) {
                            error(resp);
                        }
                    });
            },
            signOut: function() {
                tagkid.post('auth', 'signOut');
                setAsAnonymous();
            },
            activateAccount: function (ccid, cc, success, error) {
                tagkid.get('auth', 'activateAccount', {
                    ConfirmationCodeId: ccid,
                    ConfirmationCode: cc
                }, function(resp) {
                    tagkid.user({
                        Id: resp.Data.Id,
                        Username: resp.Data.Username,
                        Fullname: resp.Data.Fullname,
                        ProfileImageUrl: resp.Data.ProfileImageUrl,
                    });
                    tagkid.go('pages.timeline');
                    if (success) {
                        success(resp);
                    }
                }, function(resp) {
                    setAsAnonymous();
                    if (error) {
                        error(resp);
                    }
                });
            }
        };
    }
]);