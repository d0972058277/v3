using System;
using V3Lib.Visitors.Abstractions;

namespace V3Lib
{
    public interface IElement
    {
        Guid ComponentId { get; }
        void Accept(IVisitor visitor);
    }
}