using System.Collections.Generic;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Cache
{
    public class TagCache : CacheBase<string, Tag>
    {
        public static readonly TagCache Instance = new TagCache();

        private TagCache()
        {

        }

        protected override void Load()
        {
            IEnumerable<Tag> tags;
            using (Db.UnitOfWork())
            {
                tags = Db.TagRepository().GetAll();
            }

            foreach (var tag in tags)
            {
                base[tag.Name] = tag;
            }
        }
    }
}
