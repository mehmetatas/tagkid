using System.Linq;
using Taga.Core.Repository;
using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Repositories.Impl
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IRepository _repository;

        public TokenRepository(IRepository repository)
        {
            _repository = repository;
        }

        public Token GetById(long tokenId)
        {
            return _repository.Query<Token>()
                .FirstOrDefault(t => t.Id == tokenId);
        }

        public void Save(Token token)
        {
            _repository.Save(token);
        }
    }
}
