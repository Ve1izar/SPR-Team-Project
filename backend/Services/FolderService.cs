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

        public async Task DeleteFolderAsync(int folderId, int userId)
        {
            var children = await _context.FileItems
                .Where(f => f.ParentId == folderId && f.UserId == userId)
                .ToListAsync();

            foreach (var child in children)
            {
                if (child.Type == "Folder")
                {
                    await DeleteFolderAsync(child.Id, userId);
                }
                else
                {
                    if (System.IO.File.Exists(child.PhysicalPath))
                    {
                        System.IO.File.Delete(child.PhysicalPath);
                    }

                    _context.FileItems.Remove(child);
                }
            }

            var folder = await _context.FileItems
                .FirstOrDefaultAsync(f =>
                    f.Id == folderId &&
                    f.UserId == userId &&
                    f.Type == "Folder");

            if (folder != null)
            {
                _context.FileItems.Remove(folder);
            }

            await _context.SaveChangesAsync();
        }
    }
}