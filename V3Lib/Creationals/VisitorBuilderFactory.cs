using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using V3Lib.Creationals.Abstractions;
using V3Lib.Models;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Creationals
{
    public static class VisitorBuilderFactoryExtensions
    {
        public static IServiceCollection AddVisitorBuilderFactory(this IServiceCollection services)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(IVisitorBuilder)) && !x.IsInterface && !x.IsAbstract)).ToList();

            foreach (var type in types)
            {
                services.Add(new ServiceDescriptor(type, type, ServiceLifetime.Transient));
            }

            services.AddTransient<Dictionary<Type, IVisitorBuilder>>(sp =>
            {
                var visitorBuilders = new Dictionary<Type, IVisitorBuilder>();

                foreach (var type in types)
                {
                    var visitorBuilder = sp.GetRequiredService(type) as IVisitorBuilder;
                    visitorBuilders.Add(type, visitorBuilder);
                }

                return visitorBuilders;
            });

            services.AddTransient<VisitorBuilderFactory>();

            return services;
        }
    }

    public class VisitorBuilderFactory : ISimpleFactory
    {
        private Dictionary<Type, IVisitorBuilder> _visitorBuilders;

        public VisitorBuilderFactory(Dictionary<Type, IVisitorBuilder> visitorBuilders)
        {
            _visitorBuilders = visitorBuilders;
        }

        public T Get<T>()
        {
            var result = _visitorBuilders[typeof(T)];
            return (T) result;
        }
    }
}