using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using V3Lib.Creationals.Abstractions;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Creationals
{
    public class VisitorFactory : ISimpleFactory
    {
        private IServiceProvider _serviceProvider;

        private ISimpleFactory _this => this as ISimpleFactory;

        public VisitorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetBuilder<T>() where T : IVisitorBuilder => _this.Create<T>();

        public T GetVisitor<T>() where T : IVisitor => (T) _this.Create<IEnumerable<IVisitorBuilder>>().Select(builder => builder.Build()).Where(visitor => visitor.GetType() == typeof(T)).Single();

        T ISimpleFactory.Create<T>() => _serviceProvider.GetRequiredService<T>();
    }
}