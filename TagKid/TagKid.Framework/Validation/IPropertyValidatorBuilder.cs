
namespace TagKid.Framework.Validation
{
    public interface IPropertyValidatorBuilder<TEntity, TProperty> : IValidatorBuilder
    {
        IPropertyValidatorBuilder<TEntity, TProperty> AddRule(IValidationRule rule, object error);
    }
}