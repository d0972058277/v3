using V3Lib.Models;

namespace V3Lib.Visitors.Abstractions
{
    public interface IVisitor
    {
        void Visit(IElement element);
    }

    public interface IVisitor<T> : IVisitor where T : IElement
    {
        void Visit(T element);
    }
}