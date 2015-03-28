app.controller('UserCtrl', [
    '$scope', '$modal', '$stateParams', 'tagkid', 'userService', 'postService', function ($scope, $modal, $stateParams, tagkid, userService, postService) {
        userService.getProfile({ Username: $stateParams.username }, function (profileResp) {
            $scope.profile = profileResp.Data;

            $scope.profile.ProfileImageUrl = "/res/img/a2.jpg";
            $scope.profile.CoverImageUrl = "/res/img/cover.jpg";

            $scope.loadPosts();
        }, function () {
            alert('Unable to load prfile');
            tagkid.go('pages.timeline');
        });

        $scope.morePostsButtonText = 'Loading posts...';
        $scope.disableMorePosts = true;
        $scope.posts = [];

        $scope.loadPosts = function () {
            var maxId = 0;

            if ($scope.posts.length > 0) {
                maxId = $scope.posts[$scope.posts.length - 1].Id;
            }

            $scope.disableMorePosts = true;
            $scope.morePostsButtonText = 'Loading posts...';

            postService.getPosts({
                UserId: $scope.profile.Id,
                MaxPostId: maxId
            }, function(resp) {
                var posts = resp.Data;
                for (var i = 0; i < posts.length; i++) {
                    var post = posts[i];
                    $scope.posts.push(post);
                }

                $scope.disableMorePosts = posts.length < 10; // PageSize
                if ($scope.disableMorePosts) {
                    $scope.morePostsButtonText = 'No more posts';
                } else {
                    $scope.morePostsButtonText = 'Load more posts';
                }
            }, function() {
                $scope.disableMorePosts = false;
                $scope.morePostsButtonText = 'Load more posts';
                alert('unable to load timeline');
            });
        };
    }]);