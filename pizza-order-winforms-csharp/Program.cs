using System.Text.Json;

namespace pizza_order_winforms_csharp
{

        public class AppSettings
        {
            public decimal PizzaPriceMultiplier { get; set; } = 1.0m;
            public decimal DrinkPriceMultiplier { get; set; } = 1.0m;
            public decimal SidePriceMultiplier { get; set; } = 1.0m;
            public int DefaultPreparationSeconds { get; set; } = 10;

            private static readonly string _settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

            public static AppSettings Load()
            {
                if (!File.Exists(_settingsPath))
                {
                    var defaultSettings = new AppSettings();
                    defaultSettings.Save();
                    return defaultSettings;
                }
                string json = File.ReadAllText(_settingsPath);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }

            public void Save()
            {
                string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_settingsPath, json);
            }
        }
    


    public static class Program
    {

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new PizzaOrderSystem.MainMDIForm());
        }
    }

}