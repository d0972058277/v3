using V3Lib.Creationals.Abstractions;
using V3Lib.Models.Components;

namespace V3Lib.Models
{
    public interface IAdditional<T> : IPrototype<T>
    {
        void SetRelationComponent(Component component);
    }
}