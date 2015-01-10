namespace TagKid.Core.Models.DTO.Messages.Post
{
    public class SaveAsDraftRequest
    {
        public Database.Post Post { get; set; }
    }

    public class PublishRequest
    {
        public Database.Post Post { get; set; }
    }
}
