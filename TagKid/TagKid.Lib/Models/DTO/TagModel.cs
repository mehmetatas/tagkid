using System.Runtime.Serialization;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Models.DTO
{
    [DataContract]
    public class TagModel
    {
        public TagModel()
        {
            
        }

        public TagModel(Tag entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Hint = entity.Hint;
            Description = entity.Description;
        }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "hint")]
        public string Hint { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
