using System;
using AutoMapper;

namespace Taga.Core.Mapping
{
    using AutoMapper = AutoMapper.Mapper;

    public class Mapper : IMapper
    {
        public IMappingRuleChain<TSource, TTarget> Register<TSource, TTarget>()
            where TSource : class
            where TTarget : class
        {
            var expression = AutoMapper.CreateMap<TSource, TTarget>();
            return new MappingRuleChain<TSource, TTarget>(expression);
        }

        public TTarget Map<TTarget>(object source)
            where TTarget : class
        {
            if (source == null)
                return null;
            return AutoMapper.Map<TTarget>(source);
        }
    }

    class MappingRuleChain<TSource, TTarget> : IMappingRuleChain<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        private readonly IMappingExpression<TSource, TTarget> _expression;

        public MappingRuleChain(IMappingExpression<TSource, TTarget> expression)
        {
            _expression = expression;
        }

        public IMappingRuleChain<TSource, TTarget> Customize(Action<TSource, TTarget> customizationFunction)
        {
            _expression.AfterMap(customizationFunction);
            return this;
        }
    }
}
