namespace LocalDriveApi.Services.Interfaces
{
    public interface IFileService
    {
        Task DeleteFileAsync(int fileId, int userId);
    }
}
