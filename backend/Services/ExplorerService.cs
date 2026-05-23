using LocalDriveApi.Data;
using LocalDriveApi.Dtos;
using LocalDriveApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalDriveApi.Services
{
    public class ExplorerService :
        IExplorerService
    {
        private readonly AppDbContext _context;

        public ExplorerService(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<
            IEnumerable<SearchResultDto>>
            SearchGloballyAsync(
                int userId,
                string query)
        {
            return await _context.FileItems
                .Where(f =>
                    f.UserId == userId &&
                    f.Name.Contains(query))
                .Select(f =>
                    new SearchResultDto
                    {
                        Id = f.Id,
                        Name = f.Name,
                        Type = f.Type,
                        ParentId = f.ParentId
                    })
                .ToListAsync();
        }
    }
}