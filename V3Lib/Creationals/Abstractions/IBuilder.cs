using V3Lib.Models;

namespace V3Lib.Creationals.Abstractions
{
    public interface IBuilder { }
    public interface IBuilder<T> : IBuilder
    {
        T Build();
    }
}