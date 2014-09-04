namespace Taga.Core.Repository.Linq
{
    public interface ICrudRepository
    {
        void Save<T>(T entity);
        void Delete<T>(T entity);
        IPage<T> Select<T>(ILinqQuery<T> query);
    }
}