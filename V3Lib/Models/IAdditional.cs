using V3Lib.Models.Components;

namespace V3Lib.Models
{
    public interface IAdditional
    {
        void SetRelationComponent(Component component);
        T Clone<T>() where T : IAdditional;
    }
}