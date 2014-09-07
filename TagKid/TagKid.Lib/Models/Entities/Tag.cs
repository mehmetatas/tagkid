
namespace TagKid.Lib.Models.Entities
{
    public class Tag
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Hint { get; set; }

        public string Description { get; set; }

        public TagStatus Status { get; set; }
    }
}
