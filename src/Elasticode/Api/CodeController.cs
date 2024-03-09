using Microsoft.AspNetCore.Mvc;

namespace Elasticode;

[ApiController]
[Route("api/code")]
public class CodeController : Controller
{
    [HttpGet("classes")]
    public IActionResult GetClasses() {
        string[] classes = [
            "foo",
            "bar"
        ];

        return Ok(classes);
    }
}
