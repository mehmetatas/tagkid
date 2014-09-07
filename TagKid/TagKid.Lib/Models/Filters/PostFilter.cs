using System;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Models.Filters
{
    public class PostFilter : BaseFilter
    {
        public long UserId { get; set; }
        public long CategoryId { get; set; }
        public long[] TagIds { get; set; }
        public string Title { get; set; }
        public AccessLevel[] PostAccessLevels { get; set; }
        public AccessLevel[] CategoryAccessLevels { get; set; }

        public bool ByUser
        {
            get { return UserId > 0; }
        }

        public bool ByCategory
        {
            get { return CategoryId > 0; }
        }

        public bool ByTag
        {
            get { return TagIds != null && TagIds.Length > 0; }
        }

        public bool ByTitle
        {
            get { return !String.IsNullOrWhiteSpace(Title); }
        }

        public bool ByPostAccessLevel
        {
            get { return PostAccessLevels != null && PostAccessLevels.Length > 0; }
        }

        public bool ByCategoryAccessLevel
        {
            get { return CategoryAccessLevels != null && CategoryAccessLevels.Length > 0; }
        }
    }
}
