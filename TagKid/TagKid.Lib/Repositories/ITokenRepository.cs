using TagKid.Lib.Models.Entities;

namespace TagKid.Lib.Repositories
{
    public interface ITokenRepository
    {
        Token GetById(long tokenId);

        void Save(Token token);
    }
}
