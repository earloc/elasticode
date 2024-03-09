using Microsoft.AspNetCore.Mvc;

namespace Elasticode;

[ApiController]
[Route("api/code")]
public class CodeController : Controller
{
    [HttpGet("classes")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string[]))]
    public IActionResult GetClasses() {
        string[] classes = [
            "foo",
            "bar",
            "foobar",
            "oof",
            "rab",
            "raboof"
        ];

        return Ok(classes);
    }
}
