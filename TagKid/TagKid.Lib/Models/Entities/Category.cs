
namespace TagKid.Lib.Models.Entities
{
    public class Category
    {
        public virtual long Id { get; set; }

        public virtual long UserId { get; set; }

        public virtual string Name { get; set; }
        
        public virtual string Description { get; set; }

        public virtual AccessLevel AccessLevel { get; set; }

        public virtual CategoryStatus Status { get; set; }
    }
}
