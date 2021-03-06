﻿using System;
using System.Linq;
using System.Linq.Expressions;
using TagKid.Framework.Hosting;

namespace TagKid.Framework.Owin.Configuration
{
    public class ActionConfigurator<TService>
    {
        private readonly ControllerConfigurator _controllerConfigurator;
        private MethodMapping _currentMethodMapping;
        
        internal ActionConfigurator(ControllerConfigurator serviceConfigurator)
        {
            _controllerConfigurator = serviceConfigurator;
        }

        public ActionConfigurator<TNewService> ControllerFor<TNewService>(string controllerPath)
        {
            return _controllerConfigurator.ControllerFor<TNewService>(controllerPath);
        }

        public ActionConfigurator<TService> ActionFor(Expression<Action<TService>> actionExpression, string actionPath, HttpMethod httpMethod)
        {
            return ActionFor((LambdaExpression)actionExpression, actionPath, httpMethod);
        }

        public ActionConfigurator<TService> ActionFor<TResponse>(Expression<Func<TService, TResponse>> actionExpression, string actionPath, HttpMethod httpMethod)
        {
            return ActionFor((LambdaExpression)actionExpression, actionPath, httpMethod);
        }

        public ActionConfigurator<TService> ActionFor2<TReq>(Func<TReq, Response> actionExpression, string actionPath, HttpMethod httpMethod)
        {
            return this;
        }

        public ActionConfigurator<TService> NoAuth()
        {
            _currentMethodMapping.NoAuth = true;
            return this;
        }

        private ActionConfigurator<TService> ActionFor(LambdaExpression actionExpression, string actionPath, HttpMethod httpMethod)
        {
            var methodCallExpression = actionExpression.Body as MethodCallExpression;

            if (methodCallExpression == null)
            {
                throw new Exception("actionExpression must be MethodCallExpression!");
            }

            if (string.IsNullOrWhiteSpace(actionPath))
            {
                actionPath = methodCallExpression.Method.Name;
            }

            var method = methodCallExpression.Method;

            var parameters = method.GetParameters();
            var hasComplexTypedParameter = parameters.Any(p => p.ParameterType.IsClass && p.ParameterType != typeof(string));
            if (hasComplexTypedParameter && parameters.Length > 1)
            {
                throw new NotSupportedException("Complex typed parameter must be the only parameter of the action!");
            }
            
            _currentMethodMapping = new MethodMapping
            {
                Method = method,
                MethodRoute = actionPath,
                HttpMethod = httpMethod
            };

            _controllerConfigurator.AddMethodMapping(_currentMethodMapping);

            return this;
        }

        public ServiceConfig Build()
        {
            return _controllerConfigurator.Build();
        }
    }
}
