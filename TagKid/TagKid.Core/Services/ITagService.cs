using TagKid.Core.Models.DTO.Messages;

namespace TagKid.Core.Services
{
    public interface ITagService
    {
        TagSearchResponse Search(TagSearchRequest request);
    }
}