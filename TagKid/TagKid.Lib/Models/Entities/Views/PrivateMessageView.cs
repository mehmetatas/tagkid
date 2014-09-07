
namespace TagKid.Lib.Models.Entities.Views
{
    public class PrivateMessageView : PrivateMessage
    {
        public string FromFullname { get; set; }

        public string FromUsername { get; set; }

        public string FromProfileImageUrl { get; set; }
        
        public string ToFullname { get; set; }

        public string ToUsername { get; set; }

        public string ToProfileImageUrl { get; set; }
    }
}
