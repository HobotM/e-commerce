namespace Ecommerce.Api.Models
{
    /// <summary>
    /// Represents a product in the store.
    /// Each product has a unique ID, name, price, description, and inventory count.
    /// </summary>
    public class Product
    {
        public int Id { get; set; } // Primary Key (auto-incremented)

        public string Name { get; set; } = default!; // Name of the product

        public string Description { get; set; } = default!; // Optional product description

        public decimal Price { get; set; } // e.g., 29.99

        public int StockQuantity { get; set; } // How many units are in stock
    }
}
