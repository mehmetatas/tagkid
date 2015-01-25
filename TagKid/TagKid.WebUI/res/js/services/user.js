app.factory('userService', [
    'tagkid', function (tagkid) {
        return {
            getProfile: function (req, success, error) {
                tagkid.get('user', 'profile', req, success, error);
            }
        };
    }
]);