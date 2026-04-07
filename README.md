# 🍕 Pizza Order System – WinForms C# Application

A complete restaurant order management system built with **.NET 10.0‑Windows** and **Windows Forms**.  
Supports **multi‑language** (English / Arabic), real‑time order tracking, invoice printing, and encrypted local storage.

![C#](https://img.shields.io/badge/C%23-10.0-blue?logo=csharp)
![.NET](https://img.shields.io/badge/.NET-10.0-purple?logo=dotnet)
![WinForms](https://img.shields.io/badge/UI-WinForms-green?logo=windows)

---

## 📌 Features

### 🧑‍🍳 Order Management
- Create new orders with customer details (name, phone, address)
- Build orders by adding:
  - **Pizzas** – choose size, crust, toppings (extra charge for additional toppings)
  - **Drinks** – pick type and size (Small / Medium / Large)
  - **Sides** – Fries, Onion Rings, Garlic Bread, Salad
- Dynamic cart with item count and total calculation
- Remove items or clear entire cart

### ⏱️ Order Status & Timer
- Each order gets a preparation time (configurable via `PricesSettings.json`)
- Real‑time countdown displayed for the latest order
- Automatic status update: `Pending → Preparing → Ready → Delivered`
- Progress bar shows preparation progress
- Ready orders trigger a pop‑up notification

### 🖨️ Invoice Printing
- Print detailed invoices directly from the order history or after placing an order
- Invoice includes:
  - Restaurant header, order number, customer info
  - Itemised list (name, quantity, unit price, total)
  - Preparation details (crust, toppings, drink size)
  - Subtotal, total, thank‑you message
- Supports both LTR (English) and RTL (Arabic) layouts

### 🌍 Multi‑Language Support
- English and Arabic (العربية)
- Switch language from **Settings** form – restart required
- All UI texts, messages, and invoice content are localised
- Right‑to‑left layout adaption for Arabic

### 💾 Data Persistence
- Orders stored in `orders.json` **encrypted** using `ProtectedData` (Windows DPAPI)
- Price multipliers and default preparation time stored in `PricesSettings.json`
- User language preference saved in `App.config` via application settings

### 🖼️ Visual Enhancements
- Custom `RoundedPanel` control with border radius and title
- Animated pizza slice background on the MDI client area
- Icons for drinks, sides, and pizza (embedded resources)
- Colour‑coded total amount (green/orange/red based on value)

---

## 🧱 Project Structure

```
pizza-order-winforms-csharp/
├── Forms/
│   ├── MainMDIForm.cs              # MDI container, menu, status bar
│   ├── PizzaOrderForm.cs           # Main ordering interface
│   ├── OrderHistoryForm.cs         # View past orders, print invoices
│   └── SettingsForm.cs             # Language selection
├── Models/
│   ├── BaseItem.cs                 # Abstract item (Name, Quantity, UnitPrice)
│   ├── Pizza.cs                    # Pizza with size, crust, toppings
│   ├── Drink.cs                    # Drink with size
│   ├── Side.cs                     # Side type
│   ├── Order.cs                    # Order header + items list
│   ├── Customer.cs                 # Customer details (name, phone, address)
│   ├── Enums.cs                    # All enums (PizzaSize, CrustType, Topping, etc.)
│   └── PricesSettings.cs           # Load/save price multipliers and prep time
├── Services/
│   ├── LanguageManager.cs          # Runtime language switching
│   ├── OrderManager.cs             # Central order storage and status updates
│   ├── OrdersJsonFileStorage.cs    # Encrypted JSON read/write
│   ├── PolymorphicBaseItemConverter.cs  # JSON converter for inheritance
│   └── InvoicePrinter.cs           # PrintDocument logic for invoices
├── Resources/
│   ├── pizza.png, drink.png, side.png   # Embedded images
│   ├── pizzaico.ico                # Application icon
│   ├── Resources.resx              # Default English strings
│   └── Resources.ar.resx           # Arabic translations
├── Properties/
│   └── Settings.settings           # User scoped language setting
├── PricesSettings.json             # Configurable multipliers & prep time
├── Program.cs                      # Entry point, loads saved language
└── App.config                      # Application configuration
```

---

## 🚀 Getting Started

### Prerequisites
- [.NET 10.0 SDK](https://dotnet.microsoft.com/en-us/download) (or later)
- Windows OS (uses `System.Drawing` and `System.Security.Cryptography.ProtectedData`)

### Clone & Run

```bash
git clone https://github.com/your-repo/pizza-order-winforms-csharp.git
cd pizza-order-winforms-csharp
dotnet run
```

### First Run
- The app will create a default `PricesSettings.json` with:
  ```json
  {
    "PizzaPriceMultiplier": 1.0,
    "DrinkPriceMultiplier": 1.0,
    "SidePriceMultiplier": 1.0,
    "DefaultPreparationSeconds": 10
  }
  ```
- Orders are saved in `orders.json` (encrypted) next to the executable.

---

## 📖 How to Use

### Place a new order
1. Click **New Order** from the menu or toolbar.
2. Fill customer **Name** and **Phone** (address optional).
3. Customise your pizza:
   - Select **Size** (Small/Medium/Large/Family)
   - Choose **Crust** (Thin/Thick/Stuffed/Gluten Free)
   - Pick **Toppings** (first topping free, each extra +$1.00)
   - Set **Quantity** (1‑10)
   - Click **ADD PIZZA**
4. Add drinks and sides via the dedicated buttons (pop‑up dialogs).
5. Review the cart and click **PLACE ORDER**.
6. Optionally print the invoice.
7. The timer starts – you will be notified when the order is ready.

### View order history
- **File → Order History**
- Select a date to filter orders.
- Click an order to see its details.
- Use the **Print** button to re‑print the invoice.

### Change language
- **File → Settings** → choose English / العربية → **Save**.
- The application will restart automatically.

### Close windows
- Use **Window → Cascade / Tile Horizontal / Tile Vertical** to arrange child forms.
- **Window → Close All** closes all open order forms.

---

## ⚙️ Configuration

| File | Purpose |
|------|---------|
| `PricesSettings.json` | Multipliers for pizza/drink/side prices and default preparation time in seconds. |
| `App.config` | Stores the user’s selected language (via `userSettings`). |
| `orders.json` | Encrypted order history – do not edit manually. |

To change base prices, modify the hard‑coded values in `Pizza.cs`, `Drink.cs`, or `Side.cs` (e.g., `8.99m` for small pizza).  
The multipliers then scale those base prices.

---

## 🛠️ Technology Stack

- **Framework**: .NET 10.0‑Windows
- **UI**: Windows Forms (with custom GDI+ drawing)
- **Localisation**: `ResourceManager` + satellite assemblies (`.resx`)
- **Serialisation**: `System.Text.Json` with custom `PolymorphicBaseItemConverter`
- **Encryption**: `System.Security.Cryptography.ProtectedData` (DPAPI)
- **Printing**: `System.Drawing.Printing` (GDI+)

---

## 📸 Screenshots

> *Add actual screenshots here*  
> Example placeholders:

| Main MDI Window | Pizza Order Form |
|----------------|------------------|
| ![Main window](screenshots/main.png) | ![Order form](screenshots/order.png) |

| Order History | Invoice Print |
|---------------|----------------|
| ![History](screenshots/history.png) | ![Invoice](screenshots/invoice.png) |

---

## 📝 Future Improvements (Ideas)

- Add a **kitchen display** for pending orders
- Support for **discount coupons** or **tax** calculation
- **Export orders** to CSV / Excel
- **Customer database** with order history per customer
- **Dark mode** theme

---

## 👥 Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the **MIT License** – see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgements

- Icons and images from project resources (free for educational use)
- Inspired by real‑world restaurant POS systems

---

**Bon appétit!** 🍕🥤🍟