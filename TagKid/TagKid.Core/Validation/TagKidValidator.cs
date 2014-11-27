using FluentValidation;

namespace TagKid.Core.Validation
{
    public abstract class TagKidValidator<T> : AbstractValidator<T>
    {
        protected TagKidValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}