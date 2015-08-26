using System.Reflection;
using TagKid.Framework.Exceptions;

namespace TagKid.Framework.Validation
{
    public interface IPropertyValidator
    {
        PropertyInfo[] PropertyInfoChain { get; }
        IValidationRule Rule { get; }
        Error Error { get; }
    }
}