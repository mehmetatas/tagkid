using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Models.DTO
{
    public class RequestContext
    {
        public User User { get; set; }

        public Token AuthToken { get; set; }

        public Token RequestToken { get; set; }

        public Token NewRequestToken { get; set; }
    }
}
