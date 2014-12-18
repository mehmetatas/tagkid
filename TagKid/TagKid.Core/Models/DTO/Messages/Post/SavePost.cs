using TagKid.Core.Exceptions;
using TagKid.Core.Validation;
using TagKid.Core.Validation.Extensions;

namespace TagKid.Core.Models.DTO.Messages.Post
{
    public class SavePostRequest
    {
        public Database.Post Post { get; set; }
    }

    public class SavePostRequestValidator : TagKidValidator<SavePostRequest>
    {
        public SavePostRequestValidator()
        {
            RuleFor(r => r.Post.Category)
                .NotNull(Errors.V_SelectCategory);

            RuleFor(r => r.Post.Category.Id)
                .GreaterThan(0, Errors.V_SelectCategory);
        }
    }
}
