using TagKid.Core.Models.DTO.Messages;

namespace TagKid.Core.Service
{
    public interface ITagService
    {
        TagSearchResponse Search(TagSearchRequest request);
    }
}