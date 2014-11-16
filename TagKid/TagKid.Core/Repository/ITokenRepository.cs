using TagKid.Core.Models.Database;

namespace TagKid.Core.Repository
{
    public interface ITokenRepository : ITagKidRepository
    {
        Token Get(long tokenId);

        Token GetActiveAuthToken(long userId);

        void Save(Token token);
    }
}