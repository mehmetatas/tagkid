app.controller('mailboxCtrl', ["$scope", "$stateParams", "$state", "appMediaquery", "$window", "$timeout", function ($scope, $stateParams, $state, appMediaquery, $window, $timeout) {

    var $win = angular.element($window);

    $scope.mailPanelOpened = false;

    // Load mails in folder
    // ----------------------------------- 

    // store the current folder
    $scope.folder = $stateParams.folder || 'inbox';

    $scope.mails = [];

    // If folder wasn't loaded yet, request mails using api
    if (!$scope.mails[$scope.folder]) {

        // Replace this code with a request to your mails API
        // It expects to receive the following object format
        var tags = ["c#", "java", "python", "oop", "c++", "design-patterns", ".net", "mvc", "sql", "mysql", "php", "asp.net"];

        // only populate inbox for demo
        $scope.mails['inbox'] = [
          {
              id: 0,
              subject: 'Morbi dapibus sollicitudin',
              excerpt: 'Nunc et magna in metus pharetra ultricies ac sit amet justo. ',
              time: '09:30 am',
              from: {
                  name: 'Sass Rose',
                  email: 'mail@example.com',
                  avatar: 'app/img/user/01.jpg'
              },
              unread: false
          }
        ];
        // Generate some random user mails
        var azarnames = ['Floyd Kennedy', 'Brent Woods', 'June Simpson', 'Wanda Ward', 'Travis Hunt'];
        var azarnsubj = ['Nam sodales sollicitudin adipiscing. ', 'Cras fermentum posuere quam, sed iaculis justo rutrum at. ', 'Vivamus tempus vehicula facilisis. '];
        for (var i = 0; i < 10; i++) {
            var m = angular.copy($scope.mails['inbox'][0]);
            m.from.name = azarnames[(Math.floor((Math.random() * (azarnames.length))))];
            m.from.email = m.from.name.toLowerCase().replace(' ', '') + '@example.com';
            m.subject = azarnsubj[(Math.floor((Math.random() * (azarnsubj.length))))];
            m.from.avatar = 'app/img/user/0' + (Math.floor((Math.random() * 8)) + 1) + '.jpg';
            m.time = moment().subtract(i * (Math.floor((Math.random() * 10000)) + 1), 'minutes').fromNow();
            m.id = i + 1;

            var sentenceCount = Math.floor((Math.random() * 8)) + 1;
            while (sentenceCount-- > 0) {
                m.excerpt += azarnsubj[(Math.floor((Math.random() * (azarnsubj.length))))];
            }

            var tagCount = Math.floor((Math.random() * 5)) + 2;
            m.tags = [];
            while (tagCount-- > 0) {
                m.tags.push(tags[(Math.floor((Math.random() * (tags.length))))]);
            }

            $scope.mails['inbox'].push(m);
        }
        $scope.mails['inbox'][0].unread = true;
        $scope.mails['inbox'][1].unread = true;
        $scope.mails['inbox'][2].unread = true;
        // end random mail generation
    }

    // requested folder mails to display in the view
    $scope.mailList = $scope.mails[$scope.folder];
    
    // Show and hide mail content
    // ----------------------------------- 
    $scope.openMail = function (id) {
        // toggle mail open state
        toggleMailPanel(true);
        // load the mail into the view
        $state.go('app.mailbox.view', { id: id });
        // mark mail as read
        $timeout(function () {
            $scope.mailList[id].unread = false;
        }, 1000);
    };

    $scope.backToFolder = function () {
        toggleMailPanel(false);
    };

    // enable the open state to slide the mails panel 
    // when on table devices and below
    function toggleMailPanel(state) {
        if ($win.width() < appMediaquery['tablet'])
            $scope.mailPanelOpened = state;
    }
}]);
