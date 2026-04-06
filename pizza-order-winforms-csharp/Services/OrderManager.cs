using System;
using System.Collections.Generic;
using System.Linq;
using PizzaOrderSystem.Models;

namespace PizzaOrderSystem.Services
{
    public static class OrderManager
    {
        private static List<Order> _orders = new List<Order>();

        static OrderManager()
        {
            _orders = JsonFileStorage.LoadOrders();
            if (_orders.Any())
                Order._nextId = _orders.Max(o => o.OrderId) + 1;
        }

        public static IReadOnlyList<Order> AllOrders => _orders.AsReadOnly();

        public static void PlaceOrder(Order order)
        {
            _orders.Add(order);
            JsonFileStorage.SaveOrders(_orders);
        }

        public static void UpdateOrderStatus(int orderId, OrderStatus newStatus)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Status = newStatus;
                JsonFileStorage.SaveOrders(_orders);
            }
        }
    }
}