using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class PrivateMessageRepository : IPrivateMessageRepository
    {
        public IPage<PrivateMessageView> GetMessages(long user1, long user2, int pageIndex, int pageSize)
        {
            var sqlBuilder = Db.SqlBuilder();

            sqlBuilder.SelectAllFrom("private_message_view")
                .Where()
                .Append(" (").Equals("from_user_id", user1).And().Equals("to_user_id", user2).Append(")")
                .Or()
                .Append(" (").Equals("from_user_id", user2).And().Equals("to_user_id", user1).Append(")")
                .OrderBy("message_date", true);

            return Db.SqlRepository().ExecuteQuery<PrivateMessageView>(sqlBuilder.Build(), pageIndex, pageSize);
        }

        public void Save(PrivateMessage privateMessage)
        {
            Db.SqlRepository().Save(privateMessage);
        }
    }
}
