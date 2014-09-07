
namespace TagKid.Lib.Validation
{
    public interface IValidator<T>
    {
        void Validate(T instance);
    }
}
