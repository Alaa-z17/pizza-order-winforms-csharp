using System.Globalization;
using System.Resources;
using System.Threading;
using pizza_order_winforms_csharp.Properties;

namespace PizzaOrderSystem.Services
{
    public static class LanguageManager
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("pizza_order_winforms_csharp.Resources", typeof(LanguageManager).Assembly);

        public static event Action? LanguageChanged;

        public static void SetLanguage(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            Settings.Default.Language = cultureCode;
            Settings.Default.Save();

            LanguageChanged?.Invoke();
        }

        public static void LoadSavedLanguage()
        {
            string savedLang = Settings.Default.Language;
            if (!string.IsNullOrEmpty(savedLang) && (savedLang == "ar" || savedLang == "en"))
                SetLanguage(savedLang);
            else
                SetLanguage("en");
        }

        public static string GetString(string key)
        {
            var resource = new System.Resources.ResourceManager("pizza_order_winforms_csharp.Resources", typeof(LanguageManager).Assembly);
            string? value = resource.GetString(key, System.Threading.Thread.CurrentThread.CurrentUICulture);
            return string.IsNullOrEmpty(value) ? key : value;
        }


        public static string FormatCurrency(decimal amount)
        {
            return amount.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }
    }
}