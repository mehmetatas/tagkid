using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Taga.Core.Repository
{
    public interface IMapper
    {
        string GetTableName(Type type);

        string GetColumnName(PropertyInfo pi);
    }

    public static class MapperExtensions
    {
        public static string GetTableName<T>(this IMapper mapper) where T : class, new()
        {
            return mapper.GetTableName(typeof (T));
        }

        public static string GetColumnName<T>(this IMapper mapper, Expression<Func<T, dynamic>> propExpression)
            where T : class, new()
        {
            var pi = (PropertyInfo) ((MemberExpression) propExpression.Body).Member;
            return mapper.GetColumnName(pi);
        }
    }
}