using System.Collections.Generic;

namespace TagKid.Framework.Validation
{
    public interface IValidationScriptBuilder
    {
        string Build(IEnumerable<IPropertyValidator> validators);
    }

    public interface IJavascriptValidation
    {
        string BuildValidationScript(IValidationScriptBuilder scriptBuilder);
    }
}