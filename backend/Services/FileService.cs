using LocalDriveApi.Data;
using LocalDriveApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalDriveApi.Services
{
    public class FileService : IFileService
    {
        private readonly AppDbContext _context;

        public FileService(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteFileAsync(int fileId, int userId)
        {
            var file = await _context.FileItems
                .FirstOrDefaultAsync(f =>
                    f.Id == fileId &&
                    f.UserId == userId &&
                    f.Type == "File");

            if (file == null)
                throw new Exception("Файл не знайдено");

            if (System.IO.File.Exists(file.PhysicalPath))
            {
                System.IO.File.Delete(file.PhysicalPath);
            }

            _context.FileItems.Remove(file);

            await _context.SaveChangesAsync();
        }
    }
}
