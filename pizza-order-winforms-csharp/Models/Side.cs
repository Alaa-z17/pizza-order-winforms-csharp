namespace PizzaOrderSystem.Models
{
    public class Side : BaseItem
    {
        public SideType SideType { get; set; }
        public Side() : base("", 0) { }
        public Side(string name, int quantity, SideType type) : base(name, quantity)
        {
            SideType = type;
            var settings = pizza_order_winforms_csharp.AppSettings.LoadSettings();
            decimal basePrice = type == SideType.Fries ? 2.49m : type == SideType.OnionRings ? 3.49m : type == SideType.GarlicBread ? 2.99m : 3.99m;
            UnitPrice = basePrice * settings.SidePriceMultiplier;
        }
        public override string GetDescription() => $"{SideType}";
    }
}