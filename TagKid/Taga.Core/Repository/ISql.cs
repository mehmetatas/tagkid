
namespace Taga.Core.Repository
{
    public interface ISql
    {
        string Query { get; }

        object[] Parameters { get; }
    }
}
