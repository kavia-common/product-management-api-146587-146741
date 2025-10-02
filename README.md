# product-management-api-146587-146741

Product Backend (ASP.NET Core 8 - REST API)

- Base URL: http://localhost:3001
- API Docs: http://localhost:3001/docs
- OpenAPI JSON: http://localhost:3001/openapi.json

Endpoints
- GET /api/products — list all products
- GET /api/products/{id} — get product by id
- POST /api/products — create product (body: { name, price, quantity })
- PUT /api/products/{id} — update product (body: { name, price, quantity })
- DELETE /api/products/{id} — delete product

Notes
- In-memory repository with thread-safe storage and a few seeded items
- Validation through data annotations and a custom ValidateModel filter
- Swagger UI styled with “Ocean Professional” theme accents and available at /docs
