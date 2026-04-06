namespace PizzaOrderSystem.Models
{
    public class Pizza : BaseItem
    {
        public PizzaSize Size { get; set; }
        public CrustType Crust { get; set; }
        public List<Topping> Toppings { get; set; }


        public Pizza() : base("", 0) { }  // Parameterless constructor for JSON deserialization

        public Pizza(string name, int quantity, PizzaSize size, CrustType crust, List<Topping> toppings)
            : base(name, quantity)
        {
            Size = size;
            Crust = crust;
            Toppings = toppings ?? new List<Topping>();
            UnitPrice = CalculateUnitPrice();
        }

        private decimal CalculateUnitPrice()
        {
            var settings = pizza_order_winforms_csharp.AppSettings.LoadSettings();
            decimal price = Size switch
            {
                PizzaSize.Small => 8.99m,
                PizzaSize.Medium => 10.99m,
                PizzaSize.Large => 12.99m,
                PizzaSize.Family => 15.99m,
                _ => 10.99m
            };
            price *= settings.PizzaPriceMultiplier;
            if (Crust == CrustType.Stuffed) price += 2.00m;
            int extraToppings = Math.Max(0, Toppings.Count - 1);
            price += extraToppings * 1.00m;
            return price;
        }

        public override string GetDescription()
        {
            string toppingsStr = Toppings.Count == 0 ? "no toppings" : string.Join(", ", Toppings);
            return $"{Size} {Crust} crust pizza with {toppingsStr}";
        }
    }
}