using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Elasticode;

[ApiController]
[Route("api/code/modules")]
public class ModuleController : Controller
{
    [HttpGet("classes")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModuleModel[]))]
    public IActionResult GetModules() {
        var content = System.IO.File.ReadAllText("../../.temp/static.json");

        if (content is null) {
            return NotFound();
        }
        var modules = JsonSerializer.Deserialize<ModuleModel[]>(content);

        return Ok(modules);
    }
}
