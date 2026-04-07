using pizza_order_winforms_csharp;

namespace PizzaOrderSystem.Models
{
    public class Drink : BaseItem
    {
        public DrinkSize Size { get; set; }

        public Drink() : base("", 0) { }

        public Drink(string name, int quantity, DrinkSize size) : base(name, quantity)
        {
            Size = size;
            var settings = PricesSettings.Load();
            decimal basePrice = size == DrinkSize.Small ? 1.99m : size == DrinkSize.Medium ? 2.49m : 2.99m;
            UnitPrice = basePrice * settings.DrinkPriceMultiplier;
        }

        public override string GetDescription() => $"{Size} {Name}";
    }
}