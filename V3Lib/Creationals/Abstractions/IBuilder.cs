using V3Lib.Models;

namespace V3Lib.Creationals.Abstractions
{
    public interface IBuilder<T>
    {
        T Build();
    }
}