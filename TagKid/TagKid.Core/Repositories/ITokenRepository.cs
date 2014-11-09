using TagKid.Core.Models.Database;

namespace TagKid.Core.Repositories
{
    public interface ITokenRepository : ITagKidRepository
    {
        Token GetById(long tokenId);

        void Save(Token token);
    }
}