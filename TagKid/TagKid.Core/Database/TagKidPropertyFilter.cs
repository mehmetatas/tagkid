using System.Reflection;
using Taga.Core.Repository.Mapping;

namespace TagKid.Core.Database
{
    public class TagKidPropertyFilter : IPropertyFilter
    {
        public bool Ignore(PropertyInfo propertyInfo)
        {
            var propType = propertyInfo.PropertyType;
            return propType.IsClass && propType != typeof (string);
        }
    }
}
