using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Repositories
{
    public interface ITokenRepository : ITagKidRepository
    {
        Token GetById(long tokenId);

        void Save(Token token);
    }
}
