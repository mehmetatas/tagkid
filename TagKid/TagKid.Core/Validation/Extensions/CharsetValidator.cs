using System.Globalization;
using System.Text;
using FluentValidation.Resources;
using FluentValidation.Validators;
using System.Linq;

namespace TagKid.Core.Validation.Extensions
{
    internal class CharsetValidator : PropertyValidator
    {
        private readonly string _charset;
        private readonly bool _caseSensitive;
        private readonly CultureInfo _culture;

        public CharsetValidator(string charset, bool caseSensitive, string culture)
            : base(() => Messages.length_error)
        {
            _caseSensitive = caseSensitive;
            _culture = new CultureInfo(culture);

            _charset = _caseSensitive ? charset : charset.ToLower(_culture);
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
            {
                return false;
            }

            var str = context.PropertyValue.ToString();

            if (!_caseSensitive)
            {
                str = str.ToLower(_culture);
            }

            return str.All(c => _charset.IndexOf(c) >= 0);
        }
    }
}
