app.factory('post', [
    'tagkid', function(tagkid) {
        return {
            saveAsDraft: function(post, success, error) {
                tagkid.post('post', 'saveAsDraft', post, success, error);
            },
            getTimeline: function (req, success, error) {
                tagkid.get('post', 'timeline', req, success, error);
            }
        };
    }
]);