using System;
using System.Collections.Generic;
using V3Lib.Creationals.Abstractions;
using V3Lib.Models;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Creationals
{
    public class VisitorBuilderFactory : ISimpleFactory
    {
        private Dictionary<Type, IVisitorBuilder<IVisitor, IParams>> _visitorBuilders;

        public VisitorBuilderFactory(Dictionary<Type, IVisitorBuilder<IVisitor, IParams>> visitorBuilders)
        {
            _visitorBuilders = visitorBuilders;
        }

        public T Create<T>()
        {
            var result = _visitorBuilders[typeof(T)];
            return (T) result;
        }
    }
}