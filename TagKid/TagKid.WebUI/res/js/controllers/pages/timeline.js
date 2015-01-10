app.controller('TimelineCtrl', ['$scope', '$sce', 'tagkid', 'postService', function ($scope, $sce, tagkid, postService) {
    $('textarea').autogrow();

    $(document).on('click', '.btn-comment', function () {
        $('#comments').toggle();
    });

    $scope.user = tagkid.user();

    $scope.to_trusted = function (content) {
        return $sce.trustAsHtml(content);
    };

    $scope.morePostsButtonText = 'Loading posts...';
    $scope.disableMorePosts = true;
    $scope.posts = [];

    $scope.loadTimeline = function () {
        var maxId = 0;

        if ($scope.posts.length > 0) {
            maxId = $scope.posts[$scope.posts.length - 1].Id;
        }

        $scope.disableMorePosts = true;
        $scope.morePostsButtonText = 'Loading posts...';

        postService.getTimeline({MaxPostId: maxId},
            function (resp) {
                var posts = resp.Data;
                for (var i = 0; i < posts.length; i++) {
                    var post = posts[i];
                    post.User.ProfileImageUrl = '/res/img/a2.jpg';
                    $scope.posts.push(post);
                }

                $scope.disableMorePosts = posts.length < 1; // 1 = PageSize
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

    $scope.likeUnlike = function (post) {
        post.sendingLike = true;
        postService.likeUnlike({ PostId: post.Id },
            function (resp) {
                post.Liked = resp.Data.Liked;
                post.LikeCount = resp.Data.LikeCount;
            }, function() {
                alert('unable to like/unlike');
            }, function() {
                post.sendingLike = false;
            });
    };

    $scope.retag = function (post) {
        post.Retagged = !post.Retagged;
        post.RetagCount += post.Retagged ? 1 : -1;
    };

    $scope.loadComments = function(post) {
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

            post.disableLoadComments = comments.length < 1; // 1 = PageSize
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

    $scope.toggleComments = function (post) {
        post.ShowComments = !post.ShowComments;
        if (!post.ShowComments) {
            return;
        }

        if (!post.Comments) {
            post.Comments = [];
            $scope.loadComments(post);
        }
    };

    $scope.sendComment = function (post) {
        alert('send comment ' + post.Title + ' : ' + post.NewComment);
    };

    $scope.loadTimeline();
}]);


//comment: {
//    Content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. ',
//    PublishDate: '15 Aug 2014 Wed, 22:43',
//    User: {
//        ProfileImageUrl: '/res/img/a2.jpg',
//        Username: 'sedacetinkaya',
//        Fullname: 'Seda Çetinkaya'
//    }
//};


//post : {
//    ShowComments: false,
//    NewComment: '',
//    Title: 'title',
//    HtmlContent: '<p>Lorem ipsum dolor sit amet, consecteter adipiscing elit... Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer</p><div class="youtube-preview" style=\'background-image: url("http://img.youtube.com/vi/htobTBlCvUU/hqdefault.jpg");\'><a class="play-button" data-href="htobTBlCvUU"><i class="fa fa-4x fa-youtube-play"></i></a><img class="img-full img-responsive" style="visibility: hidden;" src="http://img.youtube.com/vi/htobTBlCvUU/hqdefault.jpg"></div><p>posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.</p>',
//    PublishDate: '15 Aug 2014 Wed, 22:34',
//    Liked: true,
//    LikeCount: 42,
//    Retagged: false,
//    RetagCount: 24,
//    CommentCount: 142,
//    Comments: [],
//    User: {
//        ProfileImageUrl: '/res/img/a2.jpg',
//        Username: 'mehmetatas',
//        Fullname: 'Mehmet Ataş'
//    },
//    Category: {
//        Id: 125,
//        Name: 'coding',
//        CssClass: 'bg-danger'
//    },
//    Tags: [
//        { Id: 1, Name: 'c#' },
//        { Id: 2, Name: 'java' },
//        { Id: 3, Name: 'unit-testing' },
//        { Id: 4, Name: 'junit' },
//        { Id: 5, Name: 'nunit' }
//    ]
//};