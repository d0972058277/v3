using System;
using V3Lib.Creationals.Abstractions;
using V3Lib.Models;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Creationals.Abstractions
{
    public interface IVisitorBuilder<T, P> : IBuilder<T> where T : IVisitor where P : IParams
    {
        Type ProductType { get; }
        P Params { get; }
        IVisitorBuilder<T, P> SetParams(P @params);
    }

    public abstract class VisitorBuilder<T, P> : IVisitorBuilder<T, P> where T : IVisitor where P : IParams
    {
        protected VisitorBuilder(P initialParams)
        {
            Params = initialParams;
        }

        public Type ProductType => typeof(T);
        public virtual P Params { get; protected set; }

        public virtual T Build() => (T) Activator.CreateInstance(ProductType, Params);
        public virtual IVisitorBuilder<T, P> SetParams(P @params)
        {
            Params = @params;
            return this;
        }
    }
}