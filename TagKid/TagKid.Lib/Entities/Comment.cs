using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKid.Lib.PetaPoco;

namespace TagKid.Lib.Entities
{
    [TableName("comments")]
    [PrimaryKey("id", autoIncrement = true)]
    [ExplicitColumns]
    public class Comment
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [Column("post_id")]
        public long PostId { get; set; }

        [Column("publish_date")]
        public DateTime PublishDate { get; set; }

        [Column("update_date")]
        public DateTime UpdateDate { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("status")]
        public CommentStatus Status { get; set; }
    }
}
