app.factory('postService', [
    'tagkid', function(tagkid) {
        return {
            saveAsDraft: function (post, success, error) {
                tagkid.post('post', 'saveAsDraft', post, success, error);
            },
            publish: function (post, success, error) {
                tagkid.post('post', 'publish', post, success, error);
            },
            getTimeline: function (req, success, error) {
                tagkid.get('post', 'timeline', req, success, error);
            },
            getCategories: function (success, error) {
                tagkid.get('post', 'categories', null, success, error);
            },
            createCategory: function (req, success, error) {
                tagkid.post('post', 'createCategory', req, success, error);
            },
            getComments: function (req, success, error, complete) {
                tagkid.get('post', 'comments', req, success, error, complete);
            },
            likeUnlike: function (req, success, error, complete) {
                tagkid.post('post', 'like', req, success, error, complete);
            }
        };
    }
]);