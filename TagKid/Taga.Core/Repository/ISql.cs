
namespace Taga.Core.Repository
{
    public interface ISql
    {
        ISql Append(string sql, params object[] parameters);
    }
}
