using V3Lib.Filters.Abstractions;
using V3Lib.Models;

namespace V3Lib.Visitors.Abstractions
{
    public interface IFilterVisitor<T> : IVisitor<T> where T : IElement
    {
        IFilter Filter { get; }
    }
}