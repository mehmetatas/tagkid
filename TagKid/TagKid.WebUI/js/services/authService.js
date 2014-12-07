app.service('authService', ['$cookieStore', '$state', function ($cookieStore, $state) {
    this.user = {
        ProfileImageUrl: '/img/a2.jpg',
        Username: 'mehmetatas',
        Fullname: 'Mehmet Ataş'
    };

    this.ensureLoggedIn = function() {
        $state.go('access.signin');
    };

    this.redirectIfLoggedIn = function() {
        
    };
}]);