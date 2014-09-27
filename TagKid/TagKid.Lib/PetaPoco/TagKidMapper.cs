using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TagKid.Lib.Models.Entities;
using TagKid.Lib.Models.Entities.Views;
using ITagaMapper = Taga.Core.Repository.IMapper;

namespace TagKid.Lib.PetaPoco
{
    public class TagKidMapper : ITagaMapper, IMapper
    {
        public TagKidMapper()
        {
            Map<User>();
            Map<Category>();
            Map<Post>();
            Map<Tag>();
            Map<PostTag>();
            Map<TagPost>();
            Map<Comment>();
            Map<Notification>();
            Map<Login>();
            Map<ConfirmationCode>();
            Map<PrivateMessage>();
            Map<Token>();

            Map<PostView>("post_search_view");
            Map<UserTagsView>("user_tags_view");
            Map<CommentView>("comment_view");
            Map<NotificationView>("notification_view");
            Map<PrivateMessageView>("private_message_view");
        }

        private void Map<T>(string tableName = null) where T : class,new()
        {
            var builder = Mapping.Map<T>(tableName);

            if (typeof(T).GetProperties().Any(p => p.Name == "Id"))
                builder.WithPrimaryKey("id");

            _mappings.Add(typeof(T), builder.Build());
        }

        private readonly Dictionary<Type, Mapping> _mappings = new Dictionary<Type, Mapping>();

        public IEnumerable<Type> Types
        {
            get { return _mappings.Keys; }
        }

        TableInfo IMapper.GetTableInfo(Type pocoType)
        {
            return _mappings[pocoType].TableInfo;
        }

        ColumnInfo IMapper.GetColumnInfo(PropertyInfo pocoProperty)
        {
            return _mappings[pocoProperty.ReflectedType].ColumnMappings[pocoProperty];
        }

        Func<object, object> IMapper.GetFromDbConverter(PropertyInfo targetProperty, Type sourceType)
        {
            return null;
        }

        Func<object, object> IMapper.GetToDbConverter(PropertyInfo sourceProperty)
        {
            return null;
        }

        string ITagaMapper.GetTableName(Type type)
        {
            return ((IMapper) this).GetTableInfo(type).TableName;
        }

        string ITagaMapper.GetColumnName(PropertyInfo pi)
        {
            return ((IMapper) this).GetColumnInfo(pi).ColumnName;
        }
    }
}
