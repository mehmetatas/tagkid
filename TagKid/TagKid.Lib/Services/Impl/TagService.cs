using System.Collections.Generic;
using System.Linq;
using Taga.Core.DynamicProxy;
using TagKid.Lib.Database;
using TagKid.Lib.Models.DTO.Messages;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Repositories;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Services.Impl
{
    [Intercept]
    public class TagService : ITagService
    {
        public TagSearchResponse Search(TagSearchRequest request)
        {
            List<Tag> tags;
            using (var db = TagKidDb.Readonly())
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