
namespace Taga.Core.Repository.Linq
{
    internal class ExpressionToSql
    {
        public static void Test()
        {
            ICrudRepository repo = null;
            ILinqQuery<User> query = new LinqSqlQuery<User>();

            var page = query
                .Where(u =>
                       !u.Name.StartsWith("ALİ") &&
                       !u.Name.EndsWith("RIZA") &&
                       !u.Name.Contains("VELİ") &&
                       !(u.Name != "ORHAN" || u.Name == "VELİ") &&
                       !u.Surname.In("KAYA", "YILMAZ", "YILDIRIM"))
                .OrderBy(u => u.Name)
                .OrderBy(u => u.Surname, true)
                .Page(3, 10)
                .Select(repo);
        }
    }
}