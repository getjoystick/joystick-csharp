using System.Threading.Tasks;
using System.Threading;
using JoystickClient.Models.ApiResponse;

namespace JoystickClient.Interfaces
{
    public interface IEnvironmentApi
    {
        Task<GetEnvironmentCatalogResponse> GetEnvironmentCatalogAsync(CancellationToken token = default);
    }
}
