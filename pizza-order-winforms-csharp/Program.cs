using PizzaOrderSystem.Services;
using PizzaOrderSystem;

namespace pizza_order_winforms_csharp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            LanguageManager.LoadSavedLanguage();
            Application.Run(new MainMDIForm());
        

        }
    }
}