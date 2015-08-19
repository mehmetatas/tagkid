using System;
using System.Text;
using TagKid.Core.Bootstrapping;
using TagKid.Framework.WebApi.Configuration;

namespace TagKid.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Bootstrapper.StartApp();

                var services = ServiceConfig.Current.ServiceMappings;

                foreach (var service in services)
                {
                    var serviceName = service.ServiceRoute;
                    var js = new StringBuilder("app.service('")
                        .Append(serviceName)
                        .AppendLine("' , ['tagkid', function (tagkid) {");

                    foreach (var method in service.MethodMappings)
                    {
                        js.Append("this.")
                            .Append(method.MethodRoute)
                            .Append("=function(request, success, error, complete) {")
                            .AppendFormat("tagkid.{0}('{1}', '{2}', request, success, error, complete);", method.HttpMethod.ToString().ToLower(), serviceName, method.MethodRoute)
                            .AppendLine("};");
                    }



                    js.Append("}]);");

                    Console.WriteLine(js);
                }

                Console.WriteLine("OK!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }
}

/*
app.service("auth", ["tagkid",
    function (tagkid) {
        this.register = function(reuqest, success, error, complete) {
            tagkid.post("auth", "register", reuqest, success, error, complete);
        };

        this.activateRegistration = function (reuqest, success, error, complete) {
            tagkid.post("auth", "activateRegistration", reuqest, success, error, complete);
        };

        this.loginWithPassword = function (reuqest, success, error, complete) {
            tagkid.post("auth", "loginWithPassword", reuqest, success, error, complete);
        };
    }]);
*/
