namespace Taga.Core.Repository.Linq
{
    public interface ILinqQueryBuilder<out TQuery>
    {
        TQuery Build();
    }
}