using Microsoft.AspNetCore.Mvc;
using product_backend.Filters;
using product_backend.Models;
using product_backend.Repositories;

namespace product_backend.Controllers
{
    /// <summary>
    /// Products controller providing CRUD operations over Product resources.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // PUBLIC_INTERFACE
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <remarks>Returns a list of all products in the store.</remarks>
        /// <response code="200">Returns the list of products</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            var items = _repository.GetAll();
            return Ok(items);
        }

        // PUBLIC_INTERFACE
        /// <summary>
        /// Retrieves a product by its id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <response code="200">Returns the product</response>
        /// <response code="404">If the product is not found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> GetById(int id)
        {
            var item = _repository.GetById(id);
            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUBLIC_INTERFACE
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="request">Product creation payload.</param>
        /// <response code="201">Returns the created product</response>
        /// <response code="400">If the payload is invalid</response>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Create([FromBody] ProductCreateDto request)
        {
            // Map DTO to Product
            var product = new Product
            {
                Name = request.Name.Trim(),
                Price = request.Price,
                Quantity = request.Quantity
            };

            _repository.Add(product);

            // Return location header of created resource
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUBLIC_INTERFACE
        /// <summary>
        /// Updates an existing product by id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="request">Product update payload.</param>
        /// <response code="200">Returns the updated product</response>
        /// <response code="400">If the payload is invalid</response>
        /// <response code="404">If the product is not found</response>
        [HttpPut("{id:int}")]
        [ValidateModel]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Update(int id, [FromBody] ProductUpdateDto request)
        {
            var existing = _repository.GetById(id);
            if (existing is null)
            {
                return NotFound();
            }

            existing.Name = request.Name.Trim();
            existing.Price = request.Price;
            existing.Quantity = request.Quantity;

            _repository.Update(existing);

            return Ok(existing);
        }

        // PUBLIC_INTERFACE
        /// <summary>
        /// Deletes a product by id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <response code="204">Product deleted</response>
        /// <response code="404">If the product is not found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deleted = _repository.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
