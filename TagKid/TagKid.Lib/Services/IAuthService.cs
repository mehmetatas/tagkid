﻿using TagKid.Lib.Models.Messages;

namespace TagKid.Lib.Services
{
    public interface IAuthService : ITagKidService
    {
        SignUpResponse SignUp(SignUpRequest request);

        void SignIn(SignInRequest request);
    }
}
