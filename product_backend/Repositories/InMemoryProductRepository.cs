using System.Collections.Concurrent;
using static System.Threading.Interlocked;
using product_backend.Models;

namespace product_backend.Repositories
{
    /// <summary>
    /// Thread-safe in-memory repository for products.
    /// </summary>
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly ConcurrentDictionary<int, Product> _store = new();
        private int _nextId = 0;

        public InMemoryProductRepository()
        {
            // Seed with a few products for convenience
            Add(new Product { Name = "Ocean Mug", Price = 12.99m, Quantity = 50 });
            Add(new Product { Name = "Amber Notebook", Price = 7.49m, Quantity = 120 });
            Add(new Product { Name = "Azure Pen", Price = 1.99m, Quantity = 500 });
        }

        public IEnumerable<Product> GetAll() => _store.Values.OrderBy(p => p.Id);

        public Product? GetById(int id) => _store.TryGetValue(id, out var p) ? p : null;

        public void Add(Product product)
        {
            var id = Interlocked.Increment(ref _nextId);
            product.Id = id;
            _store.TryAdd(product.Id, product);
        }

        public void Update(Product product)
        {
            _store.AddOrUpdate(product.Id, product, (_, __) => product);
        }

        public bool Delete(int id)
        {
            return _store.TryRemove(id, out _);
        }
    }
}
