app.constant("appDependencies", {
    // jQuery based and standalone scripts
    scripts: {
        'animate': ["lib/animate.css/animate.min.css"],
        'icons': ["lib/fontawesome/css/font-awesome.min.css"],
        'slimscroll': ["lib/slimscroll/jquery.slimscroll.min.js"],
        'moment': ["lib/moment/min/moment-with-locales.min.js"]
    },
    // Angular based script (use the right module name)
    modules: [
      {
          name: "toaster", files: ["lib/angularjs-toaster/toaster.js",
                                             "lib/angularjs-toaster/toaster.css"]
      }
    ]
})
  // Same colors as defined in the css
  .constant("appColors", {
      'primary': "#43a8eb",
      'success': "#88bf57",
      'info': "#8293b9",
      'warning': "#fdaf40",
      'danger': "#eb615f",
      'inverse': "#363C47",
      'turquoise': "#2FC8A6",
      'pink': "#f963bc",
      'purple': "#c29eff",
      'orange': "#F57035",
      'gray-darker': "#2b3d51",
      'gray-dark': "#515d6e",
      'gray': "#A0AAB2",
      'gray-light': "#e6e9ee",
      'gray-lighter': "#f4f5f5"
  })
  // Same MQ as defined in the css
  .constant("appMediaquery", {
      'desktopLG': 1240,
      'desktop': 992,
      'tablet': 768,
      'mobile': 480
  })
  // Error Codes
  .constant("err", {
      Auth_LoginRequired: 100,
      Auth_LoginTokenExpired: 101,
      Auth_UsernameAlreadyExists: 102,
      Auth_EmailAlreadyExists: 103,
      Auth_UserAwaitingActivation: 104,
      Auth_UserInactive: 105,
      Auth_UserBanned: 106,
      Auth_UsernameCannotBeEmpty: 107,
      Auth_InvalidEmailAddress: 108,
      Auth_PasswordPolicyError: 109,
      Auth_FullnameCannotBeEmpty: 110,
      Auth_InvalidLogin: 111,
      Auth_EmailOrUsernameCannotBeEmpty: 112,
      Auth_ConfirmationCodeExpired: 113,
      Auth_ConfirmationCodeAlreadyConfirmed: 114,
      Auth_ConfirmationCodeReasonMismatch: 115,
      Auth_UserNotAwaitingActivation: 116,
      Auth_ConfirmationCodeNotFound: 117,
      Auth_ConfirmationCodeMismatch: 118,
      Auth_InvalidConfirmationCodeId: 119
  });