using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PizzaOrderSystem.Models
{
    public class PricesSettings
    {
        public decimal PizzaPriceMultiplier { get; set; } = 1.0m;
        public decimal DrinkPriceMultiplier { get; set; } = 1.0m;
        public decimal SidePriceMultiplier { get; set; } = 1.0m;
        public int DefaultPreparationSeconds { get; set; } = 10;

        private static readonly string _settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PricesSettings.json");

        public static PricesSettings Load()
        {
            if (!File.Exists(_settingsPath))
            {
                var defaultSettings = new PricesSettings();
                defaultSettings.Save();
                return defaultSettings;
            }
            string json = File.ReadAllText(_settingsPath);
            return JsonSerializer.Deserialize<PricesSettings>(json) ?? new PricesSettings();
        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_settingsPath, json);
        }
    }
}