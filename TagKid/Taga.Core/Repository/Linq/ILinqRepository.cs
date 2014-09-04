namespace Taga.Core.Repository.Linq
{
    public interface ILinqRepository
    {
        void Save<T>(T entity);
        void Delete<T>(T entity);
        IPage<T> Query<T>(ILinqQueryBuilder<T> query);
    }
}