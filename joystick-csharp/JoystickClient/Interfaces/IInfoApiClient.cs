using System.Threading.Tasks;
using System.Threading;
using JoystickClient.Interfaces;

namespace JoystickClient.Clients
{
    public interface IInfoApi
    {
        Task<string> GetOpsDeckApiVersionAsync(CancellationToken token = default);
    }
}