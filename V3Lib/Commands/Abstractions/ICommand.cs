using System.Threading.Tasks;

namespace V3Lib.Commands.Abstractions
{
    public interface ICommand
    {
        IReceiver Receiver { get; }
        Task ExecuteAsync();
    }
}