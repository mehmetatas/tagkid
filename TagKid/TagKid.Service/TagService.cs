using System.Linq;
using Taga.Core.DynamicProxy;
using TagKid.Core.Database;
using TagKid.Core.Models.Database;
using TagKid.Core.Models.DTO.Messages;
using TagKid.Core.Repositories;
using TagKid.Core.Services;
using TagKid.Core.Utils;

namespace TagKid.Service
{
    [Intercept]
    public class TagService : ITagService
    {
        public TagSearchResponse Search(TagSearchRequest request)
        {
            Tag[] tags;
            using (var db = Db.Readonly())
            {
                var repo = db.GetRepository<ITagRepository>();
                tags = repo.Search(request.Filter, 1, 10).Items;
            }

            return new TagSearchResponse
            {
                Tags = tags.Select(t => t.ToModel()).ToArray()
            };
        }
    }
}