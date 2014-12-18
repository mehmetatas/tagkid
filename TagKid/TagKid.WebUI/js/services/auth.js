app.factory('auth', [
    'tagkid', function (tagkid) {
        var $redirectIfLoggedIn = function () {
            var req = {
                token: tagkid.cookies.authToken(),
                tokenId: tagkid.cookies.authTokenId()
            };

            if (req.token && req.tokenId) {
                if (tagkid.user()) {
                    tagkid.go('pages.timeline');
                } else {
                    $signInWithToken(req);
                }
            }
        };
        
        var $ensureLoggedIn = function () {
            var req = {
                token: tagkid.cookies.authToken(),
                tokenId: tagkid.cookies.authTokenId()
            };

            if (req.token && req.tokenId) {
                if (!tagkid.user()) {
                    $signInWithToken(req);
                }
                return;
            }

            tagkid.go('auth.signin');
        };

        var $signInWithToken = function(req) {
            tagkid.post('auth', 'signInWithToken', req,
                function(resp) {
                    tagkid.user({
                        Username: resp.Data.Username,
                        Fullname: resp.Data.Fullname,
                        ProfileImageUrl: resp.Data.ProfileImageUrl,
                    });
                },
                function() {
                    tagkid.user(null);
                    tagkid.cookies.authToken(null);
                    tagkid.cookies.authTokenId(null);
                    tagkid.go('auth.signin');
                });
        };
        return {
            redirectIfLoggedIn: $redirectIfLoggedIn,
            ensureLoggedIn: $ensureLoggedIn,
            signInWithToken: $signInWithToken,
            signUpWithEmail: function(req, success, error) {
                tagkid.post('auth', 'signUpWithEmail', req, success, error);
            },
            signInWithPassword: function(req) {
                tagkid.post('auth', 'signInWithPassword', req,
                    function(resp) {
                        tagkid.user({
                            Username: resp.Data.Username,
                            Fullname: resp.Data.Fullname,
                            ProfileImageUrl: resp.Data.ProfileImageUrl,
                        });
                        tagkid.go('pages.timeline');
                    });
            },
            signOut: function() {
                tagkid.post('auth', 'signOut');

                tagkid.user(null);
                tagkid.cookies.authToken(null);
                tagkid.cookies.authTokenId(null);
                tagkid.go('auth.signin');
            },
            activateAccount: function(ccid, cc, onSuccess, onFail) {
                tagkid.get('auth', 'activateAccount', {
                        ConfirmationCodeId: ccid,
                        ConfirmationCode: cc
                    },
                    function(resp) {
                        tagkid.user({
                            Username: resp.Data.Username,
                            Fullname: resp.Data.Fullname,
                            ProfileImageUrl: resp.Data.ProfileImageUrl,
                        });
                        tagkid.go('pages.timeline');
                    },
                    function(resp) {
                        tagkid.user(null);
                        tagkid.cookies.authToken(null);
                        tagkid.cookies.authTokenId(null);
                        onFail(resp);
                    });
            }
        };
    }
]);