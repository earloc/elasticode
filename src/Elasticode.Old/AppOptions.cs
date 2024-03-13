using System.ComponentModel.DataAnnotations;

namespace Elasticode;

public class AppOptions
{
    [Required]
    [Url]
    public string BaseUrl { get; set; } = "";
}
