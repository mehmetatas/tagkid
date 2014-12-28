app.controller('TimelineCtrl', ['$scope', '$sce', 'tagkid', 'post', function ($scope, $sce, tagkid, post) {
    $('textarea').autogrow();

    $(document).on('click', '.btn-comment', function () {
        $('#comments').toggle();
    });

    $scope.user = tagkid.user();

    $scope.to_trusted = function (content) {
        return $sce.trustAsHtml(content);
    };

    $scope.posts = [];

    post.getTimeline({},
        function (resp) {
            var posts = resp.Data;
            for (var i = 0; i < posts.length; i++) {
                var p = posts[i];
                p.User.ProfileImageUrl = '/res/img/a2.jpg';
                $scope.posts.push(p);
            }
        }, function () {
            alert('unable to load timeline');
        });

    //{
    //    ShowComments: false,
    //    NewComment: '',
    //    Title: 'title',
    //    Content: '<p>Lorem ipsum dolor sit amet, consecteter adipiscing elit... Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer</p><p>posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.</p>',
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
    //},
    //{
    //    ShowComments: false,
    //    NewComment: '',
    //    Title: 'Lorem ipsum dolor sit amet',
    //    Content: '<p>Lorem ipsum dolor sit amet, consecteter adipiscing elit... Lorem ipsum dolor sit amet, consectetur adipiscing elit. <br><br><img class="img-full img-responsive" src="/res/img/c5.jpg" alt="c5" /><br> Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer</p><p>posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit.  <br><br><img class="img-full img-responsive" src="/res/img/c4.jpg" alt="c4" /><br> Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.</p>',
    //    PublishDate: '29 Aug 2014 Wed, 22:34',
    //    Liked: false,
    //    LikeCount: 42,
    //    Retagged: true,
    //    RetagCount: 24,
    //    CommentCount: 0,
    //    Comments: [],
    //    User: {
    //        ProfileImageUrl: '/res/img/a2.jpg',
    //        Username: 'sedacetinkaya',
    //        Fullname: 'Seda Çetinkaya'
    //    },
    //    Category: {
    //        Id: 125,
    //        Name: 'textile',
    //        CssClass: 'bg-primary'
    //    },
    //    Tags: [
    //        { Id: 1, Name: 'denim' },
    //        { Id: 2, Name: 'blue-jean' }
    //    ]
    //},
    //{
    //    ShowComments: false,
    //    NewComment: '',
    //    Title: 'title',
    //    Content: '<p>Lorem ipsum dolor sit amet, consecteter adipiscing elit... Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer</p><div class="youtube-preview" style=\'background-image: url("http://img.youtube.com/vi/htobTBlCvUU/hqdefault.jpg");\'><a class="play-button" data-href="htobTBlCvUU"><i class="fa fa-4x fa-youtube-play"></i></a><img class="img-full img-responsive" style="visibility: hidden;" src="http://img.youtube.com/vi/htobTBlCvUU/hqdefault.jpg"></div><p>posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.</p>',
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
    //}];

    $scope.likeUnlike = function (post) {
        post.Liked = !post.Liked;
        post.LikeCount += post.Liked ? 1 : -1;
    };

    $scope.retag = function (post) {
        post.Retagged = !post.Retagged;
        post.RetagCount += post.Retagged ? 1 : -1;
    };

    $scope.toggleComments = function (post) {
        post.ShowComments = !post.ShowComments;
        post.Comments.push({
            Comment: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. ',
            CommentDate: '15 Aug 2014 Wed, 22:43',
            User: {
                ProfileImageUrl: '/res/img/a2.jpg',
                Username: 'sedacetinkaya',
                Fullname: 'Seda Çetinkaya'
            }
        });
    };

    $scope.sendComment = function (post) {
        alert('send comment ' + post.Title + ' : ' + post.NewComment);
    };
}]);