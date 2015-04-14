app.controller('TimelineCtrl', ['$scope', 'tagkid', 'postService', 'signupDialog', function ($scope, tagkid, postService, signupDialog) {

    $(document).on('click', '.btn-comment', function () {
        $('#comments').toggle();
    });

    var scrollTo = function (id) {
        setTimeout(function () {
            $(".app-content-body").scrollTo("#" + id, 1000);
        }, 100);
    };
    
    $scope.user = tagkid.user();

    $scope.removeTag = function (tag) {
        var postTags = $scope.newPost.Tags;
        for (var i = 0; i < postTags.length; i++) {
            if (postTags[i].Name == tag.Name) {
                postTags.splice(i, 1);
                break;
            }
        }

        setTimeout(function () {
            $('#tk-tag-input').focus();
        }, 100);
    };

    $scope.clear = function () {
        var post = $scope.newPost;
        if (post.Title != '' || post.HtmlContent != '' || post.Tags.length > 0) {
            if (confirm('Sure?')) {
                post.Title = '';
                post.HtmlContent = '';
                post.Tags.splice(0, post.Tags.length);
            }
        }
    };

    $scope.save = function (level) {
        var post = $scope.newPost;

        if (post.Title == '' || post.HtmlContent == '' || post.Tags.length == 0) {
            alert('Title, content and at least one tag are mandatory!');
            return;
        }

        post.AccessLevel = level;

        postService.save({
            Post: post
        }, function (resp) {
            $scope.newPost = { Tags: [] };

            if (post.Id > 0) {
                var posts = $scope.posts;
                for (var i = 0; i < posts.length; i++) {
                    if (posts[i].Id == resp.Data.Id) {
                        posts.splice(i, 1);
                        posts.splice(i, 0, resp.Data);
                        scrollTo('post' + resp.Data.Id);
                        return;
                    }
                }
            } else {
                $scope.posts.splice(0, 0, resp.Data);
            }
        });
    };

    $scope.edit = function (post) {
        $scope.newPost.Id = post.Id;
        $scope.newPost.Title = post.Title;
        $scope.newPost.HtmlContent = post.HtmlContent;
        $scope.newPost.Tags = [];

        for (var i = 0; i < post.Tags.length; i++) {
            $scope.newPost.Tags.push(post.Tags[i]);
        }

        scrollTo('postEdit');
    };

    $scope.newPost = {
        Title: '',
        HtmlContent: '',
        Tags: []
    };

    $scope.showSignupDialog = function() {
        signupDialog.show();
    };

    $scope.morePostsButtonText = 'Loading posts...';
    $scope.disableMorePosts = true;
    $scope.posts = [];

    $scope.loadTimeline = function () {
        if ($scope.user.IsAnonymous) {
            postService.getAnonymousTimeline({}, function (resp) {
                $scope.posts = resp.Data;
            }, function () {
                alert('unable to load timeline');
            });
        } else {
            var maxId = 0;

            if ($scope.posts.length > 0) {
                maxId = $scope.posts[$scope.posts.length - 1].Id;
            }

            $scope.disableMorePosts = true;
            $scope.morePostsButtonText = 'Loading posts...';

            postService.getTimeline({ MaxPostId: maxId },
                function(resp) {
                    var posts = resp.Data;
                    for (var i = 0; i < posts.length; i++) {
                        $scope.posts.push(posts[i]);
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
        }
    };

    $scope.loadTimeline();
}]);

$(function () {
    var findElementTotalOffset = function (obj) {
        var oleft = 0;
        var otop = 0;
        if (obj.offsetParent) {
            do {
                oleft += obj.offsetLeft;
                otop += obj.offsetTop;
            } while (obj = obj.offsetParent);
        }
        return { left: oleft, top: otop };
    };

    jQuery.fn.scrollTo = function (elem, speed, callback) {
        var elemOffset = findElementTotalOffset($(elem)[0]);

        $(this).animate({
            scrollTop: elemOffset.top - $(this).offset().top
        }, speed == undefined ? 1000 : speed, callback);
        return this;
    };
});

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
//    ProfileImageUrl: '/res/img/a2.jpg',
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
//        Username: 'mehmetatas',
//        Fullname: 'Mehmet Ataş'
//    },
//    Tags: [
//        { Id: 1, Name: 'c#' },
//        { Id: 2, Name: 'java' },
//        { Id: 3, Name: 'unit-testing' },
//        { Id: 4, Name: 'junit' },
//        { Id: 5, Name: 'nunit' }
//    ]
//};