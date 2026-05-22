using Microsoft.AspNetCore.Mvc;

namespace LocalDriveApi.Controllers
{
    [ApiController]
    [Route("api/storage")]
    public class StorageController : ControllerBase
    {
        private readonly string storagePath;

        public StorageController()
        {
            storagePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Storage"
            );

            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }
        }

        [HttpGet]
        public IActionResult GetFiles(
            [FromQuery] string path = ""
        )
        {
            var currentPath =
                Path.Combine(storagePath, path);

            if (!Directory.Exists(currentPath))
            {
                Directory.CreateDirectory(currentPath);
            }

            var folders =
                Directory.GetDirectories(currentPath)
                .Select(folder => new
                {
                    name = Path.GetFileName(folder),
                    isFolder = true,
                    size = 0L
                });

            var files =
                Directory.GetFiles(currentPath)
                .Select(file => new
                {
                    name = Path.GetFileName(file),
                    isFolder = false,
                    size = new FileInfo(file).Length
                });

            return Ok(
                folders.Concat(files)
            );
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            [FromForm] IFormFile file,
            [FromForm] string path = ""
        )
        {
            try
            {
                if (file == null)
                {
                    return BadRequest("file null");
                }

                var currentPath =
                    Path.Combine(storagePath, path);

                if (!Directory.Exists(currentPath))
                {
                    Directory.CreateDirectory(currentPath);
                }

                var filePath =
                    Path.Combine(
                        currentPath,
                        file.FileName
                    );

                using var stream =
                    new FileStream(
                        filePath,
                        FileMode.Create
                    );

                await file.CopyToAsync(stream);

                return Ok(new
                {
                    message = "uploaded"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-folder")]
        public IActionResult CreateFolder(
            [FromBody] FolderRequest req
        )
        {
            try
            {
                var folderPath =
                    Path.Combine(
                        storagePath,
                        req.Path ?? "",
                        req.Name
                    );

                Directory.CreateDirectory(folderPath);

                return Ok(new
                {
                    message = "created"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public IActionResult Delete(
            [FromQuery] string name,
            [FromQuery] bool isFolder,
            [FromQuery] string path = ""
        )
        {
            try
            {
                var itemPath =
                    Path.Combine(
                        storagePath,
                        path,
                        name
                    );

                if (isFolder)
                {
                    if (Directory.Exists(itemPath))
                    {
                        Directory.Delete(
                            itemPath,
                            true
                        );
                    }
                }
                else
                {
                    if (System.IO.File.Exists(itemPath))
                    {
                        System.IO.File.Delete(itemPath);
                    }
                }

                return Ok(new
                {
                    message = "deleted"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("download/{fileName}")]
        public IActionResult Download(
            string fileName,
            [FromQuery] string path = ""
        )
        {
            var filePath =
                Path.Combine(
                    storagePath,
                    path,
                    fileName
                );

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var bytes =
                System.IO.File.ReadAllBytes(filePath);

            return File(
                bytes,
                "application/octet-stream",
                fileName
            );
        }
    }

    public class FolderRequest
    {
        public string Name { get; set; } = "";
        public string? Path { get; set; }
    }
}