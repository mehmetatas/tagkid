namespace TagKid.Core.Models.Database
{
    public class SubFolderPost
    {
        public virtual SubFolder SubFolder { get; set; }
        public virtual Post Post { get; set; }
    }
}