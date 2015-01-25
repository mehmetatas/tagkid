using System;
using System.Collections;
using System.Linq;
using Taga.Core.Validation;
using TagKid.Core.Exceptions;

namespace TagKid.Core.Validation
{
    public class TagKidValidator
    {
        private static readonly Hashtable Validators = new Hashtable();

        private TagKidValidator()
        {
            
        }

        static TagKidValidator()
        {
            LoadFromAssemblyOf<TagKidValidator>();
        }

        public static void LoadFromAssemblyOf<T>()
        {
            var requestValidatorTypes = typeof(T).Assembly
                .GetTypes()
                .Where(type => typeof(IValidator).IsAssignableFrom(type) && !type.IsAbstract);

            foreach (var validatorType in requestValidatorTypes)
            {
                var requestType = validatorType.BaseType.GetGenericArguments()[0];
                Validators.Add(requestType, Activator.CreateInstance(validatorType));
            }
        }

        public static void Validate(object request)
        {
            var type = request.GetType();
            if (!Validators.ContainsKey(type))
            {
                return;
            }

            var res = ((IValidator)Validators[type]).Validate(request);

            if (res.IsValid)
            {
                return;
            }

            var error = res.Error as Error;

            if (error == null)
            {
                error = Errors.V_GenericError;
            }

            throw error.ToException();
        }
    }
}
