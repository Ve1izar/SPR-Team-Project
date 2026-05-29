using LocalDriveApi.Dtos;
using LocalDriveApi.Models;
using LocalDriveApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocalDriveApi.Controllers
{
    [ApiController]
    [Route("api/folders")]
    [Authorize]
    public class FolderController : ControllerBase
    {
        private readonly IFolderService _folderService;

        public FolderController(IFolderService folderService)
        {
            _folderService = folderService;
        }

 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFolderDto dto)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                return Unauthorized("Invalid token");

            var userId = int.Parse(claim.Value);

            var folder = await _folderService.CreateAsync(dto.Name, dto.ParentId, userId);

            return Ok(new FolderDto
            {
                Id = folder.Id,
                Name = folder.Name,
                ParentId = folder.ParentId
            });
        }


        [HttpGet]
        public async Task<IActionResult> GetRoot()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                return Unauthorized("Invalid token");

            var userId = int.Parse(claim.Value);

            var folders = await _folderService.GetByParentIdAsync(null, userId);

            var result = folders.Select(f => new FolderDto
            {
                Id = f.Id,
                Name = f.Name,
                ParentId = f.ParentId
            });

            return Ok(result);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetChildren(int id)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                return Unauthorized("Invalid token");

            var userId = int.Parse(claim.Value);

            var folders = await _folderService.GetByParentIdAsync(id, userId);

            var result = folders.Select(f => new FolderDto
            {
                Id = f.Id,
                Name = f.Name,
                ParentId = f.ParentId
            });

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                return Unauthorized("Invalid token");

            var userId = int.Parse(claim.Value);

            await _folderService.DeleteFolderAsync(id, userId);

            return Ok(new
            {
                message = "Папка успішно видалена"
            });
        }

    }
}
