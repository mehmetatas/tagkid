using Taga.Core.Repository.Sql;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Utils;

namespace TagKid.Lib.Repositories.Impl
{
    public class TokenRepository : ITokenRepository
    {
        public Token GetById(long tokenId)
        {
            return Db.SqlRepository().FirstOrDefault<Token>(t => t.Id, tokenId);
        }

        public void Save(Token token)
        {
            Db.SqlRepository().Save(token);
        }
    }
}
