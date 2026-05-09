using LocalDriveApi.Dtos;
using LocalDriveApi.Models;

namespace LocalDriveApi.Services.Interfaces
{
    public interface IFolderService
    {
        Task<Folder> CreateAsync(CreateFolderDto dto, int userId);
        Task<List<Folder>> GetByParentIdAsync(int? parentId, int userId);
    }
}
