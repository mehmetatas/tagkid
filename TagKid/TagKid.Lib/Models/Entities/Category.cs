
namespace TagKid.Lib.Models.Entities
{
    public class Category
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public CategoryStatus Status { get; set; }
    }
}
