namespace StroyMaterials.App.Models;

public sealed class LookupItem
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public override string ToString() => Name;
}
