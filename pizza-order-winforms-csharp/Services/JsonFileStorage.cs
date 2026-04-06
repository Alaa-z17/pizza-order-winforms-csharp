using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PizzaOrderSystem.Services
{
    public static class JsonFileStorage
    {
        private static readonly string _dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "orders.json");
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public static void SaveOrders(List<Models.Order> orders)
        {
            string json = JsonSerializer.Serialize(orders, _options);
            File.WriteAllText(_dataPath, json);
        }

        public static List<Models.Order> LoadOrders()
        {
            if (!File.Exists(_dataPath)) return new List<Models.Order>();
            string json = File.ReadAllText(_dataPath);
            return JsonSerializer.Deserialize<List<Models.Order>>(json, _options) ?? new List<Models.Order>();
        }
    }
}