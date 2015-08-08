namespace TagKid.Core.Models.Database
{
    public class SubFolder
    {
        public virtual long Id { get; set; }
        public virtual Folder Parent { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
    }
}