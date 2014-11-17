using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface ITokenRepository : ITagKidRepository
    {
        Token Get(long tokenId);

        void Save(Token token);

        void Delete(Token token);
    }
}