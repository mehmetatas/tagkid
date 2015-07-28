angular.module('app').service('postService', ['tagkid', function (tagkid) {
        this.save = function(data, success, error, complete) {
            return tagkid.post('post', 'save', data, success, error, complete);
        };
    }
]);