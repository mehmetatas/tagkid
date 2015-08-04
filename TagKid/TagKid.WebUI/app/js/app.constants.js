app.constant('appDependencies', {
    // jQuery based and standalone scripts
    scripts: {
        'animate': ['lib/animate.css/animate.min.css'],
        'icons': ['lib/fontawesome/css/font-awesome.min.css'],
        'sparklines': ['lib/sparklines/jquery.sparkline.min.js'],
        'wysiwyg': ['lib/bootstrap-wysiwyg/bootstrap-wysiwyg.js',
                               'lib/bootstrap-wysiwyg/external/jquery.hotkeys.js'],
        'slimscroll': ['lib/slimscroll/jquery.slimscroll.min.js'],
        'screenfull': ['lib/screenfull/dist/screenfull.min.js'],
        'moment': ['lib/moment/min/moment-with-locales.min.js']
    },
    // Angular based script (use the right module name)
    modules: [
      {
          name: 'toaster', files: ['lib/angularjs-toaster/toaster.js',
                                             'lib/angularjs-toaster/toaster.css']
      },
      {
          name: 'ui.knob', files: ['lib/angular-knob/src/angular-knob.js',
                                             'lib/jquery-knob/dist/jquery.knob.min.js']
      }
    ]
})
  // Same colors as defined in the css
  .constant('appColors', {
      'primary': '#43a8eb',
      'success': '#88bf57',
      'info': '#8293b9',
      'warning': '#fdaf40',
      'danger': '#eb615f',
      'inverse': '#363C47',
      'turquoise': '#2FC8A6',
      'pink': '#f963bc',
      'purple': '#c29eff',
      'orange': '#F57035',
      'gray-darker': '#2b3d51',
      'gray-dark': '#515d6e',
      'gray': '#A0AAB2',
      'gray-light': '#e6e9ee',
      'gray-lighter': '#f4f5f5'
  })
  // Same MQ as defined in the css
  .constant('appMediaquery', {
      'desktopLG': 1240,
      'desktop': 992,
      'tablet': 768,
      'mobile': 480
  });