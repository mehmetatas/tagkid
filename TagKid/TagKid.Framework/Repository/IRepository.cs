using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TagKid.Framework.Models.Database;
using TagKid.Framework.Repository.Mapping;

namespace TagKid.Framework.Repository
{
    public interface IRepository
    {
        void Insert<T>(T entity) where T : class, new();

        void Update<T>(T entity) where T : class, new();

        void Delete<T>(T entity) where T : class, new();

        IQueryable<T> Select<T>() where T : class, new();
    }

    public static class RepositoryExtensions
    {
        public static void Save<T>(this IRepository repo, T entity) where T : class, IEntity, new()
        {
            var isNew = entity.Id == 0L;

            if (isNew)
            {
                repo.Insert(entity);
            }
            else
            {
                repo.Update(entity);
            }
        }

        public static void Delete<TEntity, TProp>(this IAdoRepository repo, Expression<Func<TEntity, TProp>> propExpression, params TProp[] values)
            where TEntity : class, new()
        {
            MemberExpression memberExp;

            var body = propExpression.Body;
            var unaryExpression = body as UnaryExpression;
            if (unaryExpression != null)
            {
                memberExp = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExp = (MemberExpression)body;
            }

            var propInf = (PropertyInfo)memberExp.Member;

            var mappingProv = MappingProvider.Instance;

            var tableMapping = mappingProv.GetTableMapping<TEntity>();

            var columnMapping = tableMapping.Columns.First(cm => cm.PropertyInfo == propInf);

            var paramNames = Enumerable.Range(0, values.Length).Select(i => String.Format("p_{0}", i)).ToArray();

            var sql = new StringBuilder("DELETE FROM ")
                .Append(tableMapping.TableName)
                .Append(" WHERE ")
                .Append(columnMapping.ColumnName)
                .Append(" IN (")
                .Append(String.Join(",", paramNames.Select(p => String.Format("@{0}", p))))
                .Append(")")
                .ToString();

            var args = Enumerable.Range(0, values.Length).ToDictionary(i => paramNames[i], i => (object)values[i]);

            repo.ExecuteNonQuery(sql, args);
        }

        public static void Fetch<TEntity, TProp>(this IRepository repo, Expression<Func<TEntity, TProp>> propExpression, params TEntity[] entities)
            where TEntity : class, IEntity, new()
            where TProp : class, IEntity, new()
        {
            MemberExpression memberExp;

            var body = propExpression.Body;
            var unaryExpression = body as UnaryExpression;
            if (unaryExpression != null)
            {
                memberExp = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExp = (MemberExpression)body;
            }

            var refProp = (PropertyInfo)memberExp.Member;

            var fetchMapping = GetOneToOneFetchMapping(refProp);

            var dic = new Dictionary<long, List<TEntity>>();

            foreach (var entity in entities)
            {
                var key = (long)fetchMapping.KeyProperty.GetValue(entity);

                List<TEntity> tmp;
                if (dic.ContainsKey(key))
                {
                    tmp = dic[key];
                }
                else
                {
                    tmp = new List<TEntity>();
                    dic.Add(key, tmp);
                }

                tmp.Add(entity);
            }

            var propVals = repo.Select<TProp>().Where(e => dic.Keys.Contains(e.Id));

            foreach (var propVal in propVals)
            {
                refProp.SetValue(dic[propVal.Id], propVal);
            }
        }

        public static void FetchMany<TEntity, TRef>(this IRepository repo, Expression<Func<TEntity, List<TRef>>> propExpression, params TEntity[] entities)
            where TEntity : class, IEntity, new()
            where TRef : class, IEntity, new()
        {
            MemberExpression memberExp;

            var body = propExpression.Body;
            var unaryExpression = body as UnaryExpression;
            if (unaryExpression != null)
            {
                memberExp = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExp = (MemberExpression)body;
            }

            var refProp = (PropertyInfo)memberExp.Member;

            var fetchMapping = GetOneToOneFetchMapping(refProp);

            var dic = new Dictionary<long, List<TEntity>>();

            foreach (var entity in entities)
            {
                var key = (long)fetchMapping.KeyProperty.GetValue(entity);

                List<TEntity> tmp;
                if (dic.ContainsKey(key))
                {
                    tmp = dic[key];
                }
                else
                {
                    tmp = new List<TEntity>();
                    dic.Add(key, tmp);
                }

                tmp.Add(entity);
            }


            var propVals = repo.Select<TRef>().Where(e => dic.Keys.Contains(e.Id));

            foreach (var propVal in propVals)
            {
                refProp.SetValue(dic[propVal.Id], propVal);
            }
        }

        private static OneToOneFetchMapping GetOneToOneFetchMapping(PropertyInfo refProp)
        {
            return null;
        }

        public class OneToOneFetchMapping
        {
            // Post.UserId
            public PropertyInfo KeyProperty { get; set; }

            // Post.User
            public PropertyInfo RefProperty { get; set; }
        }

        public class OneToManyFetchMapping
        {
            // Like.PostId
            public PropertyInfo KeyProperty { get; set; }

            // Post.Likes
            public PropertyInfo RefProperty { get; set; }
        }

        public class ManyToManyFetchMapping
        {
            // PostTag.PostId
            public PropertyInfo ParentKeyProperty { get; set; }

            // PostTag.TagId
            public PropertyInfo ChildKeyProperty { get; set; }

            // Post.Tags
            public PropertyInfo RefProperty { get; set; }
        }

        public class FetchMappingBuilder
        {
            public void OneToOne<TEntity, TProp>(Expression<Func<TEntity, TProp>> refProp, Expression<Func<TEntity, long>> keyProp)
                where TEntity : class,IEntity, new()
                where TProp : class,IEntity, new()
            {
                //OneToOne<Post, User>(p => p.User, p => p.UserId);
            }

            public void OneToMany<TEntity, TRef>(Expression<Func<TEntity, List<TRef>>> refProp, Expression<Func<TEntity, TRef, bool>> predicate)
                where TEntity : class,IEntity, new()
                where TRef : class,IEntity, new()
            {
                OneToMany<Post, PostLike>(p => p.Likes, (p, pl) => p.Id == pl.PostId);
            }
        }

        public interface IOneToManyFecther<TEntity, TRef>
            where TEntity : class,IEntity, new()
            where TRef : class, new()
        {
            void Fetch(params TEntity[] entities);
        }

        public abstract class OneToManyFetcher<TEntity, TRef> : IOneToManyFecther<TEntity, TRef>
            where TEntity : class,IEntity, new()
            where TRef : class, new()
        {
            private readonly IRepository _repo;
            
            public OneToManyFetcher(IRepository repo)
            {
                _repo = repo;
            }

            protected abstract Expression<Func<TRef, bool>> Filter(IEnumerable<long> entityIds);
            protected abstract Func<TEntity, bool> Match(TRef refO);

            public void Fetch(params TEntity[] entities)
            {
                var entityIds = entities.Select(e => e.Id);
                var likes = _repo.Select<TRef>().Where(Filter(entityIds));

                foreach (var like in likes)
                {
                    var post = entities.First(Match(like));
                    //if (post.Likes == null)
                    //{
                    //    post.Likes = new List<PostLike>();
                    //}
                    //post.Likes.Add(like);
                }
            }
        }

        public class PostLikeFetcher //: IOneToManyFecther<Post, PostLike>
        {
            private readonly IRepository _repo;
            private readonly Func<Post, List<PostLike>> _getRefs;
            private readonly Action<Post> _initRefs;

            public PostLikeFetcher(IRepository repo, Expression<Func<Post, List<PostLike>>> refProp)
            {
                _repo = repo;
                _getRefs = refProp.Compile();
                _initRefs = p => p.Likes = new List<PostLike>();
            }

            public void Fetch(params Post[] posts)
            {
                var postIds = posts.Select(p => p.Id);
                var likes = _repo.Select<PostLike>().Where(pl => postIds.Contains(pl.PostId));

                foreach (var like in likes)
                {
                    var post = posts.First(p => p.Id == like.PostId);
                    if (_getRefs(post) == null)
                    {
                        _initRefs(post);
                    }
                    _getRefs(post).Add(like);
                }
            }
        }
    }
}
