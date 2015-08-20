using System;
using System.IO;
using System.Reflection;
using System.Text;
using TagKid.Core.Bootstrapping;
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
                    .Append("\",[\"tagkid\",function(tagkid){")
                    .Append($"var a=\"{serviceName}\";");

                foreach (var method in service.MethodMappings)
                {
                    js.Append("this.")
                        .Append(method.MethodRoute)
                        .Append("=function(r,s,e,c){")
                        .Append($"tagkid.{method.HttpMethod.ToString().ToLower()}(a,\"{method.MethodRoute}\",r,s,e,c);")
                        .Append("};");
                }

                js.Append("}]);");
            }

            File.WriteAllText(solutionDir + @"\TagKid.WebUI\app\js\services\server.js", js.ToString());
        }
    }
}
