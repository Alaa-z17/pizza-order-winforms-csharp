namespace PizzaOrderSystem.Models
{
    public class Side : BaseItem
    {
        public SideType SideType { get; set; }
        public Side(string name, int quantity, SideType type) : base(name, quantity)
        {
            SideType = type;
            UnitPrice = type == SideType.Fries ? 2.49m : type == SideType.OnionRings ? 3.49m : type == SideType.GarlicBread ? 2.99m : 3.99m;
        }
        public override string GetDescription() => $"{SideType}";
    }
}