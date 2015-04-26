var timelineCtrl = function ($scope) {
    $scope.posts = [
    {
        Id: 1,
        PublishDateText: '1d',
        Title: 'Post Title',
        Tags: [{ Name: 'tag1' }, { Name: 'tag-two' }],
        HtmlContent: '<b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! <b>This</b> <i>is</i> <u>the</u> Content! ',
        Liked: false,
        LikeCount: 21,
        ShowComments: false,
        CommentCount: 8,
        User: {
            Username: 'mehmetatas',
            Fullname: 'Mehmet Ataş',
            ProfileImageUrl: '/res/app/img/a0.jpg'
        },
        NewComment: '',
        Comments: [
        {
            User: {
                Username: 'mehmetatas',
                Fullname: 'Mehmet Ataş',
                ProfileImageUrl: '/res/app/img/a0.jpg'
            },
            PublishDate: new Date(),
            Content: 'This is the comment!'
        }, {
            User: {
                Username: 'mehmetatas',
                Fullname: 'Mehmet Ataş',
                ProfileImageUrl: '/res/app/img/a0.jpg'
            },
            PublishDate: new Date(),
            Content: 'This is the comment!'
        }, {
            User: {
                Username: 'mehmetatas',
                Fullname: 'Mehmet Ataş',
                ProfileImageUrl: '/res/app/img/a0.jpg'
            },
            PublishDate: new Date(),
            Content: 'This is the comment!'
        }, {
            User: {
                Username: 'mehmetatas',
                Fullname: 'Mehmet Ataş',
                ProfileImageUrl: '/res/app/img/a0.jpg'
            },
            PublishDate: new Date(),
            Content: 'This is the comment!'
        }, {
            User: {
                Username: 'mehmetatas',
                Fullname: 'Mehmet Ataş',
                ProfileImageUrl: '/res/app/img/a0.jpg'
            },
            PublishDate: new Date(),
            Content: 'This is the comment!'
        }, {
            User: {
                Username: 'mehmetatas',
                Fullname: 'Mehmet Ataş',
                ProfileImageUrl: '/res/app/img/a0.jpg'
            },
            PublishDate: new Date(),
            Content: 'This is the comment!'
        }, {
            User: {
                Username: 'mehmetatas',
                Fullname: 'Mehmet Ataş',
                ProfileImageUrl: '/res/app/img/a0.jpg'
            },
            PublishDate: new Date(),
            Content: 'This is the comment!'
        }, {
            User: {
                Username: 'mehmetatas',
                Fullname: 'Mehmet Ataş',
                ProfileImageUrl: '/res/app/img/a0.jpg'
            },
            PublishDate: new Date(),
            Content: 'This is the comment!'
        }, {
            User: {
                Username: 'mehmetatas',
                Fullname: 'Mehmet Ataş',
                ProfileImageUrl: '/res/app/img/a0.jpg'
            },
            PublishDate: new Date(),
            Content: 'This is the comment!'
        }, {
            User: {
                Username: 'mehmetatas',
                Fullname: 'Mehmet Ataş',
                ProfileImageUrl: '/res/app/img/a0.jpg'
            },
            PublishDate: new Date(),
            Content: 'This is the comment!'
        }, {
            User: {
                Username: 'mehmetatas',
                Fullname: 'Mehmet Ataş',
                ProfileImageUrl: '/res/app/img/a0.jpg'
            },
            PublishDate: new Date(),
            Content: 'This is the comment!'
        }, {
            User: {
                Username: 'mehmetatas',
                Fullname: 'Mehmet Ataş',
                ProfileImageUrl: '/res/app/img/a0.jpg'
            },
            PublishDate: new Date(),
            Content: 'This is the comment!'
        }]
    }];

    var scrollTo = function (id) {
        setTimeout(function () {
            $(".app-content-body").scrollTo("#" + id, 1000);
        }, 100);
    };

    $scope.editPost = function (post) {
        var clone = {
            Title: post.Title,
            HtmlContent: post.HtmlContent,
            Tags : []
        };

        for (var i = 0; i < post.Tags.length; i++) {
            clone.Tags.push(post.Tags[i]);
        }

        $scope.editorOptions.post = clone;

        scrollTo('postEdit');
    };

    $scope.editorOptions = {
        post: {
            Title: '',
            HtmlContent: '',
            Tags: []
        }
    };
};

angular.module('app').controller('timelineCtrl', ['$scope', timelineCtrl]);