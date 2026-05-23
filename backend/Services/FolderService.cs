using LocalDriveApi.Data;
using LocalDriveApi.Models;
using LocalDriveApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalDriveApi.Services
{
    public class FolderService : IFolderService
    {
        private readonly AppDbContext _context;

        public FolderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FileItem> CreateAsync(string name, int? parentId, int userId)
        {
            var folder = new FileItem
            {
                Name = name,
                Type = "Folder", 
                ParentId = parentId,
                UserId = userId,
                PhysicalPath = string.Empty,
                Size = 0
            };

            _context.FileItems.Add(folder);
            await _context.SaveChangesAsync();

            return folder;
        }

        public async Task<IEnumerable<FileItem>> GetByParentIdAsync(int? parentId, int userId)
        {
            return await _context.FileItems
                .Where(f => f.UserId == userId && f.ParentId == parentId && f.Type == "Folder")
                .ToListAsync();
        }

        public async Task<FileItem?> GetFolderByIdAsync(int id, int userId)
        {
            return await _context.FileItems
                .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId && f.Type == "Folder");
        }
    }
}