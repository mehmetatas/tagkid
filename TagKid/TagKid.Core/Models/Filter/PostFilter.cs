using System;

namespace TagKid.Core.Models.Filter
{
    public class PostFilter : BaseFilter
    {
        public long[] TagIds { get; set; }
        public string Title { get; set; }

        public bool ByTag
        {
            get { return TagIds != null && TagIds.Length > 0; }
        }

        public bool ByTitle
        {
            get { return !String.IsNullOrWhiteSpace(Title); }
        }
    }
}