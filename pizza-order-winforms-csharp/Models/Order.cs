namespace PizzaOrderSystem.Models
{
    public class Order
    {
        public static int _nextId = 1;
        public int OrderId { get; private set; }
        public Customer Customer { get; set; }
        public List<BaseItem> Items { get; set; }
        public DateTime OrderTime { get; set; }
        public OrderStatus Status { get; set; }
        public int PreparationSeconds { get; set; } = 10;

        public Order(Customer customer, List<BaseItem> items)
        {
            OrderId = _nextId++;
            Customer = customer;
            Items = items ?? new List<BaseItem>();
            OrderTime = DateTime.Now;
            Status = OrderStatus.Pending;
            var settings = PricesSettings.Load();
            PreparationSeconds = settings.DefaultPreparationSeconds;
        }

        public decimal TotalAmount => Items.Sum(i => i.CalculateTotal());
        public string Summary => $"{OrderId}: {Customer.Name} - {TotalAmount:C} - {Status}";
    }
}