using LocalDriveApi.Models;

namespace LocalDriveApi.Services.Interfaces
{
    public interface IFolderService
    {
        Task<FileItem> CreateAsync(string name, int? parentId, int userId);
        
        Task<IEnumerable<FileItem>> GetByParentIdAsync(int? parentId, int userId);
        
        Task<FileItem?> GetFolderByIdAsync(int id, int userId);

        Task DeleteFolderAsync(int folderId, int userId);
    }
}