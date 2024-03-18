namespace Elasticode;

public class ModuleModel (string name, IEnumerable<string> uses) {

    public bool IsVisible { get; set; } = true;
    public string Name { get; set; } = name;

    public IEnumerable<string> Uses { get; set; } = uses;

}
