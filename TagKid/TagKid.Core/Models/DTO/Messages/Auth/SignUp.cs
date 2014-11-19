﻿using TagKid.Core.Validation;

namespace TagKid.Core.Models.DTO.Messages.Auth
{
    public class SignUpResponse : Response
    {
    }

    public class SignUpWithEmailRequest : Request
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
    }

    public class SignUpWithEmailRequestValidator : TagKidValidator<SignUpWithEmailRequest>
    {
        public SignUpWithEmailRequestValidator()
        {
            Email(r => r.Email);
            Username(r => r.Username);
            Password(r => r.Password);
            Fullname(r => r.Fullname);
        }
    }
}
