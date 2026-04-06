using System.ComponentModel.DataAnnotations;

namespace PizzaOrderSystem.Models
{
    public class Customer
    {
        [Required(ErrorMessage = "Customer name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; } = string.Empty;

        [StringLength(250)]
        public string Address { get; set; } = string.Empty;

        public Customer() { }

        // Standard constructor for manual creation
        public Customer(string name, string phone, string address = "")
        {
            Name = name;
            Phone = phone;
            Address = address;
        }
    }
}