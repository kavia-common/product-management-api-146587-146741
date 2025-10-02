using product_backend.Models;

namespace product_backend.Repositories
{
    /// <summary>
    /// Abstraction for product persistence operations.
    /// </summary>
    public interface IProductRepository
    {
        // PUBLIC_INTERFACE
        /// <summary>Returns all products.</summary>
        IEnumerable<Product> GetAll();

        // PUBLIC_INTERFACE
        /// <summary>Returns a product by id or null if not found.</summary>
        Product? GetById(int id);

        // PUBLIC_INTERFACE
        /// <summary>Adds a product and assigns a new id.</summary>
        void Add(Product product);

        // PUBLIC_INTERFACE
        /// <summary>Updates an existing product.</summary>
        void Update(Product product);

        // PUBLIC_INTERFACE
        /// <summary>Deletes a product by id. Returns true if deleted.</summary>
        bool Delete(int id);
    }
}
