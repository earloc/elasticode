namespace Elasticode;

public class ModuleModel (string name, IEnumerable<string> uses) {

    public string Name { get; set; } = name;

    public IEnumerable<string> Uses { get; set; } = uses;

}
