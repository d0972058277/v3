using System.Threading.Tasks;

namespace V3Lib.Commands.Abstractions
{
    public interface ICommandWithResult<T> : ICommand
    {
        T Result { get; }
        new Task<T> ExecuteAsync();
    }
}