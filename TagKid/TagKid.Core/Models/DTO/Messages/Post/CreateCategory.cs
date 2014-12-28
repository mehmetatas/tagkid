using TagKid.Core.Exceptions;
using TagKid.Core.Models.Database;
using TagKid.Core.Validation;
using TagKid.Core.Validation.Extensions;

namespace TagKid.Core.Models.DTO.Messages.Post
{
    public class CreateCategoryRequest
    {
        public Category Category { get; set; }
    }
}
