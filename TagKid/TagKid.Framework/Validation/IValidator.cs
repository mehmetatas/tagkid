
namespace TagKid.Framework.Validation
{
    public interface IValidator
    {
        ValidationResult Validate(object instance);
    }
}
