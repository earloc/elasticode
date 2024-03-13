using Microsoft.AspNetCore.Mvc;

namespace Elasticode;

[ApiController]
[Route("api/code/modules")]
public class ModuleController : Controller
{
    [HttpGet("classes")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModuleModel[]))]
    public IActionResult GetModules() {
        ModuleModel[] classes = [
            new("foo", []),
            new("bar", []),
            new("foobar", ["foo", "bar"]),
            new("oof", ["rab"]),
            new("rab", []),
            new("oofrab", ["foobar", "oof"]),
        ];

        return Ok(classes);
    }
}
