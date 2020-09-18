using V3Lib.Models;

namespace V3Lib.Creationals.Abstractions
{
    public interface IBuilder
    {
        object Build();
    }
    public interface IBuilder<T> : IBuilder
    {
        new T Build();
    }
}