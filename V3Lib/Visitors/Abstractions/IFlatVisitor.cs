using System;
using System.Collections.Generic;
using V3Lib.Models;

namespace V3Lib.Visitors.Abstractions
{
    public interface IFlatVisitor<T> : IVisitor<T> where T : IElement
    {
        Dictionary<Guid, T> FlatElements { get; }
    }
}