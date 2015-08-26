using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TagKid.Core.Bootstrapping;
using TagKid.Framework.Validation;
using TagKid.Framework.WebApi.Configuration;

namespace TagKid.ConsoleApp
{
    static class PostBuild
    {
        public static void Run()
        {
            Bootstrapper.StartApp();

            var solutionDir = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory?.Parent?.Parent?.Parent?.FullName;
            if (solutionDir == null)
            {
                throw new InvalidOperationException("cannot reach to solutionDir!");
            }

            CreateServerJs(solutionDir);
        }

        private static void CreateServerJs(string solutionDir)
        {
            Console.WriteLine("Creating server.js");

            var services = ServiceConfig.Current.ServiceMappings;

            var js = new StringBuilder();

            foreach (var service in services)
            {
                var serviceName = service.ServiceRoute;

                js.Append("app.service(\"")
                    .Append(serviceName)
                    .Append("\",[\"tagkid\",\"validator\",\"err\",function(t,v,err){")
                    .Append($"var a=\"{serviceName}\";");

                foreach (var method in service.MethodMappings)
                {
                    var requestParam = method.Method.GetParameters()[0];

                    var paramType = requestParam.ParameterType;

                    var validator = ValidationManager.GetValidator(paramType) as IJavascriptValidation;

                    js.Append("this.")
                        .Append(method.MethodRoute)
                        .Append("=function(r,s,e,c){");

                    if (validator != null)
                    {
                        var validationScript = validator.BuildValidationScript(new TagKidValidationScriptBuilder("r", "e", "v", "err"));
                        js.Append(validationScript);
                    }

                    js.Append($"t.{method.HttpMethod.ToString().ToLower()}(a,\"{method.MethodRoute}\",r,s,e,c);")
                        .Append("};");
                }

                js.Append("}]);");
            }

            File.WriteAllText(solutionDir + @"\TagKid.WebUI\app\js\services\server.js", js.ToString());
        }
    }

    class TagKidValidationScriptBuilder : IValidationScriptBuilder
    {
        private readonly string _requestObjectVariableName;
        private readonly string _errorCallbackName;
        private readonly string _validatorVariableName;
        private readonly string _errorConstantsVariableName;

        public TagKidValidationScriptBuilder(string requestObjectVariableName, string errorCallbackName, string validatorVariableName, string errorConstantsVariableName)
        {
            _requestObjectVariableName = requestObjectVariableName;
            _errorCallbackName = errorCallbackName;
            _validatorVariableName = validatorVariableName;
            _errorConstantsVariableName = errorConstantsVariableName;
        }

        public string Build(IEnumerable<IPropertyValidator> validators)
        {
            var js = new StringBuilder();

            foreach (var validator in validators)
            {
                js.Append("if(!(");
                for (var i = 0; i < validator.PropertyInfoChain.Length - 1; i++)
                {
                    js.Append(_requestObjectVariableName);
                    for (var j = 0; j <= i; j++)
                    {
                        var prop = validator.PropertyInfoChain[j];
                        js.Append($".{prop.Name}");
                    }
                    js.Append("&&");
                }

                js.Append(_validatorVariableName)
                    .Append(".execute")
                    .Append(validator.Rule.GetType().Name)
                    .Append("(")
                    .Append(_requestObjectVariableName)
                    .Append(".")
                    .Append(String.Join(".", validator.PropertyInfoChain.Select(p => p.Name)))
                    .Append("))) return (")
                    .Append(_errorCallbackName)
                    .Append("||window.alert)(")
                    .Append(_errorConstantsVariableName)
                    .Append(".")
                    .Append(validator.Error.MessageCode)
                    .Append(");");
            }

            return js.ToString();
        }

    }
}
