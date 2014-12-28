app.factory('post', [
    'tagkid', function(tagkid) {
        return {
            saveAsDraft: function(post, success, error) {
                tagkid.post('post', 'saveAsDraft', post, success, error);
            },
            getTimeline: function (req, success, error) {
                tagkid.get('post', 'timeline', req, success, error);
            },
            getCategories: function (success, error) {
                tagkid.get('post', 'categories', null, success, error);
            },
            createCategory: function (req, success, error) {
                tagkid.post('post', 'createCategory', req, success, error);
            }
        };
    }
]);