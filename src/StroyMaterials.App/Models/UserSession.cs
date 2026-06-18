namespace StroyMaterials.App.Models;

internal sealed class UserSession
{
    public static UserSession Guest { get; } = new()
    {
        FullName = "Гость",
        RoleName = "Гость"
    };

    public int? UserId { get; init; }

    public string FullName { get; init; } = "";

    public string RoleName { get; init; } = "";

    public bool IsGuest => UserId is null;

    public bool CanFilterProducts => RoleName is "Менеджер" or "Администратор";

    public bool CanEditProducts => RoleName == "Администратор";

    public bool CanViewOrders => RoleName is "Менеджер" or "Администратор";

    public bool CanEditOrders => RoleName == "Администратор";
}
