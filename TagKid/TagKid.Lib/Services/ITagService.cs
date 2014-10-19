using TagKid.Lib.Models.DTO.Messages;

namespace TagKid.Lib.Services
{
    public interface ITagService
    {
        TagSearchResponse Search(TagSearchRequest request);
    }
}