namespace PizzaOrderSystem.Models
{
    public enum PizzaSize { Small, Medium, Large, Family }
    public enum CrustType { Thin, Thick, Stuffed, GlutenFree }
    public enum Topping { Pepperoni, Mushrooms, Onions, Sausage, ExtraCheese, Olives, GreenPepper }
    public enum DrinkSize { Small, Medium, Large }
    public enum SideType { Fries, OnionRings, GarlicBread, Salad }
    public enum OrderStatus { Pending, Preparing, Ready, Delivered }
}