using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using TagKid.Framework.Json;
using TagKid.Framework.Utils;

namespace TagKid.Framework.WebApi.Impl
{
    public class ParameterResolver : IParameterResolver
    {
        private readonly IJsonSerializer _json;

        public ParameterResolver(IJsonSerializer json)
        {
            _json = json;
        }

        public void Resolve(RouteContext ctx)
        {
            ctx.Parameters = ResolveParameters(ctx);
        }

        private object[] ResolveParameters(RouteContext routeContext)
        {
            var parameters = routeContext.Method.Method.GetParameters();

            if (parameters.Length == 0)
            {
                return new object[0];
            }

            var isSingleComplexTypedParameter = parameters.Length == 1 &&
                parameters[0].ParameterType.IsClass &&
                parameters[0].ParameterType != typeof(string);

            if (isSingleComplexTypedParameter)
            {
                return ResolveComplexTypedParameter(routeContext, parameters[0].ParameterType);
            }

            if (parameters.Length == 1 && routeContext.DefaultParameter != null)
            {
                return ResolvePrimitiveTypedParameterFromDefaultParameter(routeContext.DefaultParameter, parameters);
            }

            return ResolvePrimitiveTypedParametersFromQueryString(routeContext, parameters);
        }

        private object[] ResolveComplexTypedParameter(RouteContext routeContext, Type complexParamType)
        {
            var requestBody = routeContext.Request.Content.ReadAsStringAsync().Result;

            var paramValue = String.IsNullOrWhiteSpace(requestBody)
                ? ResolveComplexTypedParameterFromQueryString(routeContext, complexParamType)
                : ResolveComplexTypedParameterFromRequestBody(requestBody, complexParamType);

            return new[] { paramValue };
        }

        private object ResolveComplexTypedParameterFromQueryString(RouteContext routeContext, Type complexParamType)
        {
            var value = Activator.CreateInstance(complexParamType);
            var queryStringParams = routeContext.Request.GetQueryNameValuePairs()
                .Select(p => new { p.Key, p.Value })
                .ToList();

            foreach (var propInf in complexParamType.GetProperties())
            {
                var queryStringParam = queryStringParams
                    .FirstOrDefault(qp => String.Equals(qp.Key, propInf.Name, StringComparison.InvariantCultureIgnoreCase));

                if (queryStringParam == null)
                {
                    continue;
                }

                propInf.SetValue(value, SafeParse(queryStringParam.Value, propInf.PropertyType));
            }

            return value;
        }

        private object ResolveComplexTypedParameterFromRequestBody(string requestBody, Type complexParamType)
        {
            return _json.Deserialize(requestBody, complexParamType);
        }

        private object[] ResolvePrimitiveTypedParameterFromDefaultParameter(string defaultParam, ParameterInfo[] parameters)
        {
            return new[] { SafeParse(defaultParam, parameters[0].ParameterType) };
        }

        private object[] ResolvePrimitiveTypedParametersFromQueryString(RouteContext routeContext, ParameterInfo[] parameters)
        {
            var parameterValues = new object[parameters.Length];

            foreach (var parameter in parameters)
            {
                var found = false;
                foreach (var pair in routeContext.Request.GetQueryNameValuePairs())
                {
                    if (pair.Key == parameter.Name)
                    {
                        parameterValues[parameter.Position] = SafeParse(pair.Value, parameter.ParameterType);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    parameterValues[parameter.Position] = parameter.ParameterType.IsValueType
                        ? Activator.CreateInstance(parameter.ParameterType)
                        : null;
                }
            }

            return parameterValues;
        }

        private object SafeParse(string value, Type targetType)
        {
            return QueryStringUtil.Parse(value, targetType);
        }
    }
}
