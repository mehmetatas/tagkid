using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TagKid.Framework.Json.Newtonsoft
{
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = AllPropertiesResolver.Instance,
            MaxDepth = 50,
            TypeNameHandling = TypeNameHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, SerializerSettings);
        }

        public object Deserialize(string json, Type targetType)
        {
            return JsonConvert.DeserializeObject(json, targetType, SerializerSettings);
        }

        class AllPropertiesResolver : DefaultContractResolver
        {
            public static readonly AllPropertiesResolver Instance = new AllPropertiesResolver();

            private readonly IDictionary<Type, List<MemberInfo>> _cache = new ConcurrentDictionary<Type, List<MemberInfo>>();

            protected override List<MemberInfo> GetSerializableMembers(Type objectType)
            {
                if (_cache.ContainsKey(objectType))
                {
                    return _cache[objectType];
                }

                var members = objectType.GetProperties()
                    .Cast<MemberInfo>()
                    .ToList();

                _cache.Add(objectType, members);

                return members;
            }
        }
    }
}
