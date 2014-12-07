app.controller('TimelineCtrl', ['$scope', '$modal', '$http', 'authService', function ($scope, $modal, $http, authService) {
    authService.ensureLoggedIn();

    $('textarea').autogrow();

    $(document).on('click', '.btn-comment', function () {
        $('#comments').toggle();
    });

    $scope.user = authService.user;

    $scope.posts = [
    {
        ShowComments: false,
        NewComment: '',
        Title: 'title',
        Content: '<p>Lorem ipsum dolor sit amet, consecteter adipiscing elit... Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer</p><p>posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.</p>',
        PublishDate: '15 Aug 2014 Wed, 22:34',
        Liked: true,
        LikeCount: 42,
        Retagged: false,
        RetagCount: 24,
        CommentCount: 142,
        Comments: [],
        User: {
            ProfileImageUrl: '/img/a2.jpg',
            Username: 'mehmetatas',
            Fullname: 'Mehmet Ataş'
        },
        Category: {
            Id: 125,
            Name: 'coding',
            CssClass: 'bg-danger'
        },
        Tags: [
            { Id: 1, Name: 'c#' },
            { Id: 2, Name: 'java' },
            { Id: 3, Name: 'unit-testing' },
            { Id: 4, Name: 'junit' },
            { Id: 5, Name: 'nunit' }
        ]
    }];

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
                ProfileImageUrl: '/img/a2.jpg',
                Username: 'sedacetinkaya',
                Fullname: 'Seda Çetinkaya'
            }
        });
    };

    $scope.sendComment = function (post) {
        alert('send comment ' + post.Title + ' : ' + post.NewComment);
    };
}]);

//app.filter('propsFilter', function () {
//    return function (items, props) {
//        var out = [];

//        if (angular.isArray(items)) {
//            items.forEach(function (item) {
//                var itemMatches = false;

//                var keys = Object.keys(props);
//                for (var i = 0; i < keys.length; i++) {
//                    var prop = keys[i];
//                    var text = props[prop].toLowerCase();
//                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
//                        itemMatches = true;
//                        break;
//                    }
//                }

//                if (itemMatches) {
//                    out.push(item);
//                }
//            });
//        } else {
//            // Let the output be the input untouched
//            out = items;
//        }

//        return out;
//    };
//});