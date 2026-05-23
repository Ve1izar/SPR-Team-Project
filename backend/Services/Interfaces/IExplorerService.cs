using LocalDriveApi.Dtos;

namespace LocalDriveApi.Services.Interfaces
{
    public interface IExplorerService
    {
        Task<IEnumerable<SearchResultDto>>
            SearchGloballyAsync(
                int userId,
                string query);
    }
}