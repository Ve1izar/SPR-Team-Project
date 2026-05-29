using LocalDriveApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
    private readonly IFileService _fileService;

    public FilesController(IFileService fileService)
    {
        _fileService = fileService;

        if (!Directory.Exists(_storagePath))
            Directory.CreateDirectory(_storagePath);
    }

    [HttpGet]
    public IActionResult GetFiles()
    {
        var directory = new DirectoryInfo(_storagePath);
        var files = directory.GetFiles().Select(f => new
        {
            Name = f.Name,
            Size = (f.Length / 1024.0).ToString("F2") + " KB",
            Extension = f.Extension.ToLower()
        });

        return Ok(files);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest();

        var filePath = Path.Combine(_storagePath, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok();
    }

    [HttpGet("download/{fileName}")]
    public IActionResult Download(string fileName)
    {
        var filePath = Path.Combine(_storagePath, fileName);
        if (!System.IO.File.Exists(filePath)) return NotFound();

        var bytes = System.IO.File.ReadAllBytes(filePath);
        return File(bytes, "application/octet-stream", fileName);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized();

        await _fileService.DeleteFileAsync(id, userId);

        return Ok(new
        {
            message = "Файл успішно видалено"
        });
    }
}