angular.module('app')
    .directive('tagkidPost', ['$compile', '$sce', 'tagkid', 'postService', function ($compile, $sce, tagkid, postService) {
        var likeUnlike = function (post) {
            post.sendingLike = true;
            postService.likeUnlike({ PostId: post.Id },
                function (resp) {
                    post.Liked = resp.Data.Liked;
                    post.LikeCount = resp.Data.LikeCount;
                }, function () {
                    alert('unable to like/unlike');
                }, function () {
                    post.sendingLike = false;
                });
        };

        var retag = function (post) {
            post.Retagged = !post.Retagged;
            post.RetagCount += post.Retagged ? 1 : -1;
        };

        var loadComments = function (post) {
            var maxId = 0;
            if (post.Comments.length > 0) {
                maxId = post.Comments[post.Comments.length - 1].Id;
            }

            post.disableLoadComments = true;
            post.moreCommentsButtonText = "Loading comments...";

            postService.getComments({
                PostId: post.Id,
                MaxCommentId: maxId
            }, function (resp) {
                var comments = resp.Data;
                for (var i = 0; i < comments.length; i++) {
                    var comment = comments[i];
                    comment.User.ProfileImageUrl = '/res/img/a2.jpg';
                    post.Comments.push(comment);
                }

                post.disableLoadComments = comments.length < 10; // 10 = PageSize
                if (post.disableLoadComments) {
                    post.moreCommentsButtonText = 'No more comments';
                } else {
                    post.moreCommentsButtonText = 'Load more comments';
                }
            }, function () {
                post.disableLoadComments = false;
                post.moreCommentsButtonText = "Load more comments";
                alert('unable to get comments!');
            });
        };

        var toggleComments = function (post) {
            post.ShowComments = !post.ShowComments;
            if (!post.ShowComments) {
                return;
            }

            if (!post.Comments) {
                post.Comments = [];
                loadComments(post);
            }
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
                postService.postComment({
                    PostId: post.Id,
                    Comment: post.NewComment
                }, function (resp) {
                    post.NewComment = '';
                    var newComment = resp.Data;
                    newComment.User.ProfileImageUrl = '/res/img/a2.jpg';
                    post.Comments.splice(0, 0, newComment);
                });
            }
        };

        var linker = function (scope, element, attrs) {
            scope.likeUnlike = likeUnlike;
            scope.retag = retag;
            scope.loadComments = loadComments;
            scope.toggleComments = toggleComments;
            scope.likeUnlike = likeUnlike;
            scope.to_trusted = to_trusted;
            scope.commentKeyDown = commentKeyDown;
            scope.user = tagkid.user();

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