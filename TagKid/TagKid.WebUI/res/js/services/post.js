app.factory('postService', [
    'tagkid', function(tagkid) {
        return {
            save: function (post, success, error) {
                tagkid.post('post', 'save', post, success, error);
            },
            getTimeline: function (req, success, error) {
                tagkid.get('post', 'timeline', req, success, error);
            },
            getPosts: function (req, success, error) {
                tagkid.get('post', 'posts', req, success, error);
            },
            getComments: function (req, success, error, complete) {
                tagkid.get('post', 'comments', req, success, error, complete);
            },
            likeUnlike: function (req, success, error, complete) {
                tagkid.post('post', 'like', req, success, error, complete);
            },
            searchTags: function (req, success, error, complete) {
                tagkid.get('post', 'searchTags', req, success, error, complete);
            }
        };
    }
]);