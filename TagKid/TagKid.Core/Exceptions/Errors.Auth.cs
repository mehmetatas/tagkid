﻿using TagKid.Framework.Exceptions;

namespace TagKid.Core.Exceptions
{
    public static partial class Errors
    {
        public static readonly Error Auth_LoginRequired = new Error(100, "Auth_LoginRequired");
        public static readonly Error Auth_LoginTokenExpired = new Error(101, "Auth_LoginTokenExpired");
        public static readonly Error Auth_UsernameAlreadyExists = new Error(102, "Auth_UsernameAlreadyExists");
        public static readonly Error Auth_EmailAlreadyExists = new Error(103, "Auth_EmailAlreadyExists");
        public static readonly Error Auth_UserAwaitingActivation = new Error(104, "Auth_UserAwaitingActivation");
        public static readonly Error Auth_UserInactive = new Error(105, "Auth_UserInactive");
        public static readonly Error Auth_UserBanned = new Error(106, "Auth_UserBanned");
        public static readonly Error Auth_UsernameCannotBeEmpty = new Error(107, "Auth_UsernameCannotBeEmpty");
        public static readonly Error Auth_InvalidEmailAddress = new Error(108, "Auth_InvalidEmailAddress");
        public static readonly Error Auth_PasswordPolicyError = new Error(109, "Auth_PasswordPolicyError");
        public static readonly Error Auth_FullnameCannotBeEmpty = new Error(110, "Auth_FullnameCannotBeEmpty");
        public static readonly Error Auth_InvalidLogin = new Error(111, "Auth_InvalidLogin");
        public static readonly Error Auth_EmailOrUsernameCannotBeEmpty = new Error(112, "Auth_EmailOrUsernameCannotBeEmpty");
        public static readonly Error Auth_ConfirmationCodeExpired = new Error(113, "Auth_ConfirmationCodeExpired");
        public static readonly Error Auth_ConfirmationCodeAlreadyConfirmed = new Error(114, "Auth_ConfirmationCodeAlreadyConfirmed");
        public static readonly Error Auth_ConfirmationCodeReasonMismatch = new Error(115, "Auth_ConfirmationCodeReasonMismatch");
        public static readonly Error Auth_UserNotAwaitingActivation = new Error(116, "Auth_UserNotAwaitingActivation");
        public static readonly Error Auth_ConfirmationCodeNotFound = new Error(117, "Auth_ConfirmationCodeNotFound");
        public static readonly Error Auth_ConfirmationCodeMismatch = new Error(118, "Auth_ConfirmationCodeMismatch");
        public static readonly Error Auth_InvalidConfirmationCodeId = new Error(119, "Auth_InvalidConfirmationCodeId");        
    }
}
