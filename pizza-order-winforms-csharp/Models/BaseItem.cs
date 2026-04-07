using System;

namespace PizzaOrderSystem.Models
{
    public abstract class BaseItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; protected set; }

        protected BaseItem(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public virtual decimal CalculateTotal() => UnitPrice * Quantity;
        public abstract string GetDescription();
    }
}