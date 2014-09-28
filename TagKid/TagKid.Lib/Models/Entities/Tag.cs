
using System;

namespace TagKid.Lib.Models.Entities
{
    public class Tag
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Hint { get; set; }

        public string Description { get; set; }

        public TagStatus Status { get; set; }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Hint.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{0} ({1}): {2}", Name, Hint, Description);
        }
    }
}
