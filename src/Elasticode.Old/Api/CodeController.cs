using Microsoft.AspNetCore.Mvc;

namespace Elasticode;

public class ClassModel (string name, IEnumerable<string> members) {

    public string Name { get; set; } = name;

    public IEnumerable<string> Uses { get; set; } = members;

}

[ApiController]
[Route("api/code")]
public class CodeController : Controller
{
    [HttpGet("classes")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassModel[]))]
    public IActionResult GetClasses() {
        ClassModel[] classes = [
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
