using Microsoft.AspNetCore.Mvc;

namespace Elasticode;

public class ClassModel (string name, IEnumerable<string> members) {

    public string Name { get; set; } = name;

    public IEnumerable<string> Members { get; set; } = members;

}

[ApiController]
[Route("api/code")]
public class CodeController : Controller
{
    [HttpGet("classes")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassModel[]))]
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
