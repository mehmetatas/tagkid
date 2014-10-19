
namespace TagKid.Lib.Models.Entities.Views
{
    public class PrivateMessageView : PrivateMessage
    {
        public virtual string FromFullname { get; set; }

        public virtual string FromUsername { get; set; }

        public virtual string FromProfileImageUrl { get; set; }
        
        public virtual string ToFullname { get; set; }

        public virtual string ToUsername { get; set; }

        public virtual string ToProfileImageUrl { get; set; }
    }
}
