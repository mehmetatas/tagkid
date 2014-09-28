using System.Linq;
using Taga.Core.DynamicProxy;
using TagKid.Lib.Cache;
using TagKid.Lib.Models.DTO;
using TagKid.Lib.Models.DTO.Messages;

namespace TagKid.Lib.Services.Impl
{
    [Intercept]
    public class TagService : ITagService
    {
        public TagSearchResponse Search(TagSearchRequest request)
        {
            return new TagSearchResponse
            {
                Tags = TagNameCache.Instance.Search(request.Filter)
                    .Select(t => new TagModel(t))
                    .ToArray()
            };
        }
    }
}
