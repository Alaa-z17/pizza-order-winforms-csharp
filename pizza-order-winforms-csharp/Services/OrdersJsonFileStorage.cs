using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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
            byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
            byte[] encryptedBytes = ProtectedData.Protect(jsonBytes, null, DataProtectionScope.CurrentUser);
            string encryptedBase64 = Convert.ToBase64String(encryptedBytes);
            File.WriteAllText(_dataPath, encryptedBase64);
        }

        public static List<Order> LoadOrders()
        {
            if (!File.Exists(_dataPath))
                return new List<Order>();

            string fileContent = File.ReadAllText(_dataPath);

            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(fileContent);
                byte[] decryptedBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
                string json = Encoding.UTF8.GetString(decryptedBytes);
                return JsonSerializer.Deserialize<List<Order>>(json, _options) ?? new List<Order>();
            }
            catch (FormatException)
            {
                try
                {
                    var orders = JsonSerializer.Deserialize<List<Order>>(fileContent, _options) ?? new List<Order>();
                    if (orders.Count > 0)
                        SaveOrders(orders);
                    return orders;
                }
                catch (JsonException)
                {
                    return new List<Order>();
                }
            }
            catch (CryptographicException)
            {
                try
                {
                    return JsonSerializer.Deserialize<List<Order>>(fileContent, _options) ?? new List<Order>();
                }
                catch
                {
                    return new List<Order>();
                }
            }
        }
    }
}