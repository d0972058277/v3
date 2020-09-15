using V3Lib.Models;

namespace V3Lib.Visitors.Abstractions
{
    public abstract class VisitorBase<T> : IVisitor<T> where T : IElement
    {
        public abstract void Visit(T element);
        public virtual void Visit(IElement element) => this.Visit((T) element);
    }
}