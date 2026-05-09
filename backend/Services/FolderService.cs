using LocalDriveApi.Data;
using LocalDriveApi.Dtos;
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

        public async Task<Folder> CreateAsync(CreateFolderDto dto, int userId)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("Назва папка є обов'язковою");

            int? parentId = dto.ParentId;

            if (parentId.HasValue)
            {
                var parentExists = await _context.Folders
                    .AnyAsync(f =>
                        f.Id == parentId.Value &&
                        f.UserId == userId);

                if (!parentExists)
                    throw new Exception("Батьківська папка не знайдена");
            }

            var folder = new Folder
            {
                Name = dto.Name,
                ParentId = parentId,
                UserId = userId
            };

            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();

            return folder;
        }

        public async Task<List<Folder>> GetByParentIdAsync(int? parentId, int userId)
        {
            return await _context.Folders
                .Where(f =>
                    f.ParentId == parentId &&
                    f.UserId == userId)
                .ToListAsync();
        }
    }
}
