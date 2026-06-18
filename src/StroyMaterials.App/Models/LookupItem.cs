namespace StroyMaterials.App.Models;

internal sealed class LookupItem
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public override string ToString() => Name;
}
