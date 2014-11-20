using System;
using System.Linq.Expressions;
using FluentValidation;
using TagKid.Core.Exceptions;
using TagKid.Core.Utils;
using TagKid.Core.Validation.Extensions;

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