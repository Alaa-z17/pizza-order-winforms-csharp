namespace PizzaOrderSystem.Models
{
    public class Drink : BaseItem
    {
        public DrinkSize Size { get; set; }
        public Drink(string name, int quantity, DrinkSize size) : base(name, quantity)
        {
            Size = size;
            UnitPrice = size == DrinkSize.Small ? 1.99m : size == DrinkSize.Medium ? 2.49m : 2.99m;
        }
        public override string GetDescription() => $"{Size} {Name}";
    }
}
