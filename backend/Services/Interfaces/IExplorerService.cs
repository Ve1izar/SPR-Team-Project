using LocalDriveApi.Dtos;

namespace LocalDriveApi.Services
{
    public interface IExplorerService
    {
        Task<IEnumerable<SearchResultDto>> SearchGloballyAsync(int userId, string query);
    }
}