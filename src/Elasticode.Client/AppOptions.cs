using System.ComponentModel.DataAnnotations;

namespace Elasticode.Client;

public class AppOptions
{
    [Required]
    [Url]
    public string BaseUrl { get; set; } = "";
}
