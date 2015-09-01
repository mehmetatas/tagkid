using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TagKid.ConsoleApp
{
    static class PostBuild
    {
        public static void Run(string solutionDir)
        {
            Console.WriteLine("SolutionDir " + solutionDir);

            var webUiBin = Path.Combine(solutionDir, @"TagKid.WebUI\bin");
            
            var coreDllPath = Path.Combine(webUiBin, "TagKid.Core.dll");
            var coreDll = Assembly.LoadFrom(coreDllPath);

            var fwDllPath = Path.Combine(webUiBin, "TagKid.Framework.dll");
            var fwDll = Assembly.LoadFrom(fwDllPath);

            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => fwDll;

            foreach (var referencedAssembly in coreDll.GetReferencedAssemblies())
            {
                Assembly.Load(referencedAssembly);
            }

            var bootstrapperType = coreDll.GetType("TagKid.Core.Bootstrapping.Bootstrapper");
            if (bootstrapperType == null)
            {
                Console.WriteLine("Cannot resolve type: TagKid.Core.Bootstrapping.Bootstrapper");
                return;
            }

            var svcConfigType = fwDll.GetType("TagKid.Framework.WebApi.Configuration.ServiceConfig");
            if (svcConfigType == null)
            {
                Console.WriteLine("Cannot resolve type: TagKid.Framework.WebApi.Configuration.ServiceConfig");
                return;
            }

            var errorsType = coreDll.GetType("TagKid.Core.Exceptions.Errors");
            if (errorsType == null)
            {
                Console.WriteLine("Cannot resolve type: TagKid.Core.Exceptions.Errors");
                return;
            }

            bootstrapperType.GetMethod("StartApp").Invoke(null, null);

            var svcConfig = svcConfigType.GetProperty("Current").GetValue(null);

            CreateServerJs(solutionDir, svcConfig);



            CreateErrorsJs(solutionDir, errorsType);
        }

        private static void CreateErrorsJs(string solutionDir, Type errorsType)
        {
            Console.WriteLine("Creating errors.js");

            var js = new StringBuilder()
                .Append("app.service(\"errors\",[\"translate\",function(t){");


            var errors = errorsType.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => (dynamic)f.GetValue(null));

            foreach (var error in errors)
            {
                js.Append($"this.{error.MessageCode}")
                    .Append("={code:")
                    .Append(error.Code)
                    .Append(",getMessage:function(){return ")
                    .Append($"t.get(\"{error.MessageCode}\",arguments);")
                    .Append("}};");
            }

            js.Append("}]);");

            File.WriteAllText(solutionDir + @"\TagKid.WebUI\app\js\services\errors.js", js.ToString());
        }

        private static void CreateServerJs(string solutionDir, dynamic serviceConfig)
        {
            Console.WriteLine("Creating server.js");

            var services = serviceConfig.ServiceMappings;

            var js = new StringBuilder();

            foreach (var service in services)
            {
                var serviceName = service.ServiceRoute;

                js.Append("app.service(\"")
                    .Append(serviceName)
                    .Append("\",[\"tagkid\",function(t){")
                    .Append($"var a=\"{serviceName}\";");

                foreach (var method in service.MethodMappings)
                {
                    js.Append("this.")
                        .Append(method.MethodRoute)
                        .Append("=function(r,s,e,c){")
                        .Append($"t.{method.HttpMethod.ToString().ToLower()}(a,\"{method.MethodRoute}\",r,s,e,c);")
                        .Append("};");
                }

                js.Append("}]);");
            }

            File.WriteAllText(solutionDir + @"\TagKid.WebUI\app\js\services\server.js", js.ToString());
        }
    }

    //class TagKidValidationScriptBuilder : IValidationScriptBuilder
    //{
    //    private readonly string _requestObjectVariableName;
    //    private readonly string _errorCallbackName;
    //    private readonly string _validatorVariableName;
    //    private readonly string _errorConstantsVariableName;

    //    public TagKidValidationScriptBuilder(string requestObjectVariableName, string errorCallbackName, string validatorVariableName, string errorConstantsVariableName)
    //    {
    //        _requestObjectVariableName = requestObjectVariableName;
    //        _errorCallbackName = errorCallbackName;
    //        _validatorVariableName = validatorVariableName;
    //        _errorConstantsVariableName = errorConstantsVariableName;
    //    }

    //    public string Build(IEnumerable<IPropertyValidator> validators)
    //    {
    //        var js = new StringBuilder();

    //        foreach (var validator in validators)
    //        {
    //            js.Append("if(!(");
    //            for (var i = 0; i < validator.PropertyInfoChain.Length - 1; i++)
    //            {
    //                js.Append(_requestObjectVariableName);
    //                for (var j = 0; j <= i; j++)
    //                {
    //                    var prop = validator.PropertyInfoChain[j];
    //                    js.Append($".{prop.Name}");
    //                }
    //                js.Append("&&");
    //            }

    //            js.Append(_validatorVariableName)
    //                .Append(".execute")
    //                .Append(validator.Rule.GetType().Name)
    //                .Append("(")
    //                .Append(_requestObjectVariableName)
    //                .Append(".")
    //                .Append(String.Join(".", validator.PropertyInfoChain.Select(p => p.Name)))
    //                .Append("))) return (")
    //                .Append(_errorCallbackName)
    //                .Append("||window.alert)(")
    //                .Append(_errorConstantsVariableName)
    //                .Append(".")
    //                .Append(validator.Error.MessageCode)
    //                .Append(");");
    //        }

    //        return js.ToString();
    //    }
    //}
}
