'use strict';

angular.module('app', [
    'ngAnimate',
    'ngCookies',
    'ngResource',
    'ngSanitize',
    'ngTouch',
    'ngStorage',
    'ui.router',
    'ui.bootstrap',
    'ui.load',
    'textAngular',
    //'ui.jq',
    'ui.validate'
    //'oc.lazyLoad',
    //'pascalprecht.translate'
])
.config(function($provide){
    $provide.decorator('taOptions', ['taRegisterTool', '$delegate', function(taRegisterTool, taOptions){
        // $delegate is the taOptions we are decorating
        // register the tool with textAngular
        taRegisterTool('colourRed', {
            iconclass: "fa fa-square red",
            action: function () {
                var url = prompt('Please enter the image image url:', 'http://');
                var html = '<img class="img-responsive" src="' + url + '" />';
                this.$editor().wrapSelection('inserthtml', html);
            }
        });
        // add the button to the default toolbar definition
        taOptions.toolbar[1].push('colourRed');
        return taOptions;
    }]);
});