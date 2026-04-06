namespace pizza_order_winforms_csharp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new PizzaOrderSystem.MainMDIForm());
        }
    }
}