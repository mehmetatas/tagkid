using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace TagKid.Lib.Validation
{
    public static class Validator
    {
        private static readonly Dictionary<Type, IValidator> Validators;

        static Validator()
        {
            var validatorTypes =
                typeof (Validator).Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof (IValidator)));
            Validators = validatorTypes.ToDictionary(
                t => t.BaseType.GetGenericArguments()[0],
                t => (IValidator) Activator.CreateInstance(t));
        }

        public static void Validate<T>(T instance)
        {
            var res = Validators[typeof (T)].Validate(instance);

            if (res.IsValid)
                return;

            throw new ValidationException(String.Join(Environment.NewLine, res.Errors.Select(e => e.ErrorMessage)));
        }
    }
}