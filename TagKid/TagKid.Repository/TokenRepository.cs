using System.Linq;
using Taga.Core.Repository;
using TagKid.Core.Models.Database;
using TagKid.Core.Repository;

namespace TagKid.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IRepository _repository;

        public TokenRepository(IRepository repository)
        {
            _repository = repository;
        }

        public Token Get(long tokenId)
        {
            return _repository.Select<Token>()
                .FirstOrDefault(t => t.Id == tokenId);
        }

        public void Save(Token token)
        {
            _repository.Save(token);
        }

        public void Delete(Token token)
        {
            _repository.Delete(token);
        }

        public void DeleteTokensOfUser(long userId)
        {
            _repository.Delete<Token>(t => t.UserId, userId);
        }

        public void Flush()
        {
            _repository.Flush();
        }
    }
}