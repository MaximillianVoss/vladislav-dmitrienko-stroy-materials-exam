namespace StroyMaterials.App.UI;

internal sealed record SortOption(string Key, string Name)
{
    public override string ToString() => Name;
}
