namespace StroyMaterials.App.Models;

public sealed class OrderRecord
{
    public int Id { get; set; }

    public string ItemsText { get; set; } = "";

    public DateTime OrderDate { get; set; } = DateTime.Today;

    public DateTime DeliveryDate { get; set; } = DateTime.Today;

    public int PickupPointId { get; set; }

    public string PickupPointAddress { get; set; } = "";

    public string CustomerName { get; set; } = "";

    public int ReceiveCode { get; set; }

    public int StatusId { get; set; }

    public string StatusName { get; set; } = "";
}
