using TagKid.Core.Exceptions;
using TagKid.Framework.Validation;

namespace TagKid.Core.Models.Messages.Post
{
    public class SaveRequest
    {
        public Database.Post Post { get; set; }
    }

    public class SaveRequestValidator : Validator<SaveRequest>
    {
        protected override void BuildRules()
        {
            RuleFor(r => r.Post.User.Email).Email(Errors.Auth_InvalidEmailAddress);
        }
    }
}
