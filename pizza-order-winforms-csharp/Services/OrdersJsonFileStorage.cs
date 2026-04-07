using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using PizzaOrderSystem.Models;

namespace PizzaOrderSystem.Services
{
    public static class OrdersJsonFileStorage
    {
        private static readonly string _dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "orders.json");
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter(), new PolymorphicBaseItemConverter() }
        };

        public static void SaveOrders(List<Order> orders)
        {
            string json = JsonSerializer.Serialize(orders, _options);
            File.WriteAllText(_dataPath, json);
        }

        public static List<Order> LoadOrders()
        {
            if (!File.Exists(_dataPath)) return new List<Order>();
            string json = File.ReadAllText(_dataPath);
            return JsonSerializer.Deserialize<List<Order>>(json, _options) ?? new List<Order>();
        }
    }
}