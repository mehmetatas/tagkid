angular.module('app')
    .directive('tagkidPost', ['$sce', function ($sce) {
        var likeUnlike = function (post) {
            alert('like unlike');
        };
        
        var loadComments = function (post) {
            alert('load comments');
        };

        var toggleComments = function (post) {
            post.ShowComments = !post.ShowComments;
        };

        var to_trusted = function (content) {
            return $sce.trustAsHtml(content);
        };

        var commentKeyDown = function (e, post) {
            if (e.keyCode == 13 && !e.shiftKey) {
                e.preventDefault();
                if (!post.NewComment || post.NewComment.length < 10) {
                    alert('at least 10 characters please!');
                    return;
                }
                alert('post comment');
            }
        };

        var linker = function (scope, element, attrs) {
            scope.likeUnlike = likeUnlike;
            scope.loadComments = loadComments;
            scope.toggleComments = toggleComments;
            scope.to_trusted = to_trusted;
            scope.commentKeyDown = commentKeyDown;

            scope.user = {
                Username: 'mehmetatas'
            };

            scope.edit = function (p) {
                scope.onEdit({ post: p });
            };
        };

        return {
            restrict: 'E',
            link: linker,
            scope: {
                post: '=',
                onEdit: '&'
            },
            templateUrl: '/Directives/Post'
        };
    }]);