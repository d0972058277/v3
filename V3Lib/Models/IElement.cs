using System;
using V3Lib.Filters.Abstractions;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Models
{
    public interface IElement
    {
        Guid Id { get; }
        void Accept(IVisitor visitor);
    }
}