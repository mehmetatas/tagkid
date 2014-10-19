namespace TagKid.Lib.Models.Filters
{
    public abstract class BaseFilter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}