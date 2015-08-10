using System.Collections.Generic;

namespace TagKid.Core.Models.Database
{
    public class Folder
    {
        public virtual long Id { get; set; }
        public virtual User User { get; set; }
        public virtual string Name { get; set; }
        public virtual string Icon { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }

        public virtual List<SubFolder> SubFolders { get; set; }
    }
}