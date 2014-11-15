namespace TagKid.Core.Models.Filter
{
    public abstract class BaseFilter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}