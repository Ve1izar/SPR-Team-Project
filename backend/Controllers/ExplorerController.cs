using Microsoft.AspNetCore.Mvc;

namespace LocalDriveApi.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly string _uploadPath;

        public FilesController()
        {
            _uploadPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Uploads");

            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        [HttpGet]
        public IActionResult GetFiles()
        {
            var files = Directory
                .GetFiles(_uploadPath)
                .Select(file => new
                {
                    name = Path.GetFileName(file),
                    size = new FileInfo(file).Length
                });

            return Ok(files);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Файл пустий");
            }

            var filePath = Path.Combine(
                _uploadPath,
                file.FileName);

            using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create);

            await file.CopyToAsync(stream);

            return Ok(new
            {
                message = "uploaded"
            });
        }

        [HttpGet("download/{fileName}")]
        public IActionResult Download(
            string fileName)
        {
            var filePath = Path.Combine(
                _uploadPath,
                fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var bytes =
                System.IO.File.ReadAllBytes(filePath);

            return File(
                bytes,
                "application/octet-stream",
                fileName);
        }
    }
}