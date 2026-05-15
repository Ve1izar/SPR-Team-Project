using LocalDriveApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocalDriveApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExplorerController : ControllerBase
    {
        private readonly IExplorerService _explorerService;

        public ExplorerController(IExplorerService explorerService)
        {
            _explorerService = explorerService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest(new { error = "Запит порожній" });

            // Отримання ID користувача з JWT
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                             ?? User.FindFirst("id")?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var results = await _explorerService.SearchGloballyAsync(userId, query);
            return Ok(results);
        }
    }
}