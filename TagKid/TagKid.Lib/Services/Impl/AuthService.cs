using Taga.Core.DynamicProxy;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Exceptions;
using TagKid.Lib.Models.DTO.Messages;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Services.Impl
{
    [Intercept]
    public class AuthService : IAuthService
    {
        public virtual SignUpResponse SignUp(SignUpRequest request)
        {
            using (var uow = Db.UnitOfWork())
            {
                Validate(request);

                var userRepo = Db.UserRepository();

                var existing = userRepo.GetByUsername(request.Username);
                if (existing != null)
                    ValidateStatus(existing.Status, request.Username, "User");

                existing = userRepo.GetByEmail(request.Email);
                if (existing != null)
                    ValidateStatus(existing.Status, request.Email, "Email");

                var user = new User
                {
                    Username = request.Username,
                    Password = request.Password,
                    Email = request.Email,
                    Fullname = request.Fullname,
                    FacebookId = request.FacebookId,
                    ProfileImageUrl = "",
                    Type = UserType.User,
                    Status = UserStatus.Active
                };

                userRepo.Save(user);

                uow.Save();
            }

            return new SignUpResponse();
        }

        public virtual void SignIn(SignInRequest request)
        {
            throw new System.NotImplementedException();
        }

        private void ValidateStatus(UserStatus status, string fieldValue, string fieldName)
        {
            if (status == UserStatus.Active)
                throw new UserException("{0} {1} already exists!", fieldName, fieldValue);
            if (status == UserStatus.Passive)
                throw new UserException("{0} {1} is passive!", fieldName, fieldValue);
            if (status == UserStatus.AwaitingActivation)
                throw new UserException("{0} {1} has not been actived yet!", fieldName, fieldValue);
            if (status == UserStatus.Banned)
                throw new UserException("{0} {1} is banned!", fieldName, fieldValue);
        }

        private void Validate(SignUpRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
