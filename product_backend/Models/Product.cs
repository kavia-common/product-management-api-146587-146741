using System.ComponentModel.DataAnnotations;

namespace product_backend.Models
{
    /// <summary>
    /// Product entity representing a stock keeping unit.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique identifier of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Human-friendly display name of the product.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Unit price for the product.
        /// </summary>
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Available quantity in stock.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }

    /// <summary>
    /// DTO used for creating a product.
    /// </summary>
    public class ProductCreateDto
    {
        /// <summary>
        /// Human-friendly display name of the product.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Unit price for the product.
        /// </summary>
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Available quantity in stock.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }

    /// <summary>
    /// DTO used for updating a product.
    /// </summary>
    public class ProductUpdateDto
    {
        /// <summary>
        /// Human-friendly display name of the product.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Unit price for the product.
        /// </summary>
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Available quantity in stock.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
