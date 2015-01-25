app.controller('UserCtrl', [
    '$scope', '$modal', '$stateParams', 'tagkid', 'userService', 'postService', function ($scope, $modal, $stateParams, tagkid, userService, postService) {
        userService.getProfile({ Username: $stateParams.username }, function (profileResp) {
            $scope.profile = profileResp.Data;

            $scope.profile.ProfileImageUrl = "/res/img/a2.jpg";
            $scope.profile.CoverImageUrl = "/res/img/cover.jpg";

            loadCategories();
        }, function () {
            alert('Unable to load prfile');
            tagkid.go('pages.timeline');
        });

        var loadCategories = function () {
            postService.getCategories({ UserId: $scope.profile.Id }, function (resp) {
                $scope.categories.splice(0, $scope.categories.length);
                for (var i = 0; i < resp.Data.length; i++) {
                    var cat = resp.Data[i];

                    $scope.categories.push(cat);

                    if (cat.Name == $stateParams.category) {
                        $scope.selectedCategory = cat;
                    }
                }
                $scope.loadPosts();
            });
        };

        $scope.categories = [];
        $scope.selectedCategory = null;

        $scope.showAllCategories = function () {
            $scope.posts.splice(0, $scope.posts.length);
            $scope.selectedCategory = null;
            $scope.loadPosts();
        }

        $scope.selectCategory = function (categoryId) {
            $scope.posts.splice(0, $scope.posts.length);
            for (var i = 0; i < $scope.categories.length; i++) {
                var cat = $scope.categories[i];
                if (cat.Id == categoryId) {
                    $scope.selectedCategory = cat;
                    break;
                }
            }
            $scope.loadPosts();
        };

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
                MaxPostId: maxId,
                CategoryId: $scope.selectedCategory ? $scope.selectedCategory.Id : 0
            },
                function (resp) {
                    var posts = resp.Data;
                    for (var i = 0; i < posts.length; i++) {
                        var post = posts[i];
                        post.User.ProfileImageUrl = '/res/img/a2.jpg';
                        $scope.posts.push(post);
                    }

                    $scope.disableMorePosts = posts.length < 10; // PageSize
                    if ($scope.disableMorePosts) {
                        $scope.morePostsButtonText = 'No more posts';
                    } else {
                        $scope.morePostsButtonText = 'Load more posts';
                    }
                }, function () {
                    $scope.disableMorePosts = false;
                    $scope.morePostsButtonText = 'Load more posts';
                    alert('unable to load timeline');
                });
        };
    }]);