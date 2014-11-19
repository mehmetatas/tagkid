﻿using System.Collections.Generic;
using TagKid.Core.Utils;

namespace TagKid.Core.Exceptions
{
    public static class Errors
    {
        public static readonly Error Unknown = new Error(-1, new Dictionary<string, string>
        {
            { EnGb, "Unknown error has occured!" },
            { TrTr, "Bilinmeyen bir hata oluştu!" },
        });

        #region Validation

        public static readonly Error Validation_GenericError = new Error(1, new Dictionary<string, string>
        {
            { EnGb, "Please check the values you entered!" },
            { TrTr, "Lütfen girdiğiniz değerleri kontrol ediniz!" },
        });
        public static readonly Error Validation_Password = new Error(2, new Dictionary<string, string>
        {
            { EnGb, "Invalid password! Your password must be 6 - 20 characters long." },
            { TrTr, "Geçersiz şifre! Şifreniz 6 - 20 karakter uzunluğunda olmalı." },
        });
        public static readonly Error Validation_Username = new Error(3, new Dictionary<string, string>
        {
            { EnGb, "Invalid username! Username must be 1 - 16 characters long and can only contain alpha-numeric characters, dash (-) and underscore (_)." },
            { TrTr, "!" },
        });
        public static readonly Error Validation_EmailAddress = new Error(4, new Dictionary<string, string>
        {
            { EnGb, "Invalid email address! Email adress must be 5 - 80 characters long." },
            { TrTr, "!" },
        });
        public static readonly Error Validation_Fullname = new Error(5, new Dictionary<string, string>
        {
            { EnGb, "Invalid Fullname! Fullname can have maximum 50 characters." },
            { TrTr, "!" },
        });
        public static readonly Error Validation_EmailAlreadyExists = new Error(6, new Dictionary<string, string>
        {
            { EnGb, "Email address already exists! Did you forgot your password?" },
            { TrTr, "!" },
        });
        public static readonly Error Validation_UsernameAlreadyExists = new Error(7, new Dictionary<string, string>
        {
            { EnGb, "Username already exists! Did you forgot your password?" },
            { TrTr, "!" },
        });

        #endregion

        #region Security

        public static readonly Error Security_InvalidAuthToken = new Error(100, new Dictionary<string, string>
        {
            { EnGb, "!" },
            { TrTr, "!" },
        });
        public static readonly Error Security_UserInactive = new Error(101, new Dictionary<string, string>
        {
            { EnGb, "!" },
            { TrTr, "!" },
        });

        #endregion

        private const string EnGb = Cultures.EnGb;
        private const string TrTr = Cultures.TrTr;
    }
}