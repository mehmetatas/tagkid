using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using TagKid.Core.Exceptions;

namespace TagKid.Core.Validation
{
    public static class Validator
    {
        private static readonly Dictionary<Type, IValidator> Validators;

        static Validator()
        {
            var validatorTypes =
                typeof (Validator).Assembly.GetTypes().Where(t => !t.IsAbstract && t.GetInterfaces().Contains(typeof (IValidator)));

            Validators = validatorTypes.ToDictionary(
                t => t.BaseType.GetGenericArguments()[0],
                t => (IValidator) Activator.CreateInstance(t));
        }

        public static void Validate(object request)
        {
            var type = request.GetType();
            if (!Validators.ContainsKey(type))
            {
                return;
            }

            var res = Validators[type].Validate(request);

            if (res.IsValid)
            {
                return;   
            }

            if (res.Errors[0].CustomState != null)
            {
                E.x((Error)res.Errors[0].CustomState);
            }

            E.x(Errors.Validation_GenericError);
        }
    }
}