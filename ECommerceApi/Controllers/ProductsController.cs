using ECommerceApi.DTOs.Products;
using ECommerceApi.Models;
using ECommerceApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    /// <summary>
    ///     Get a product by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await productService.GetProductByIdAsync(id);
        return product is null ? NotFound() : Ok(product);
    }

    /// <summary>
    ///     Get all products
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var products = await productService.GetAllProductsAsync();
        return Ok(products);
    }

    /// <summary>
    ///     Get products by category
    /// </summary>
    [HttpGet("category/{categoryId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCategory(Guid categoryId)
    {
        return Ok(await productService.GetProductsByCategoryAsync(categoryId));
    }

    /// <summary>
    ///     Search products by name, description, or brand
    /// </summary>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return BadRequest("Search term cannot be empty.");

        return Ok(await productService.SearchProductsAsync(term));
    }

    /// <summary>
    ///     Create a new product
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var createdProduct = await productService.CreateProductAsync(dto);
        return createdProduct is null
            ? BadRequest()
            : CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }

    /// <summary>
    ///     Update a product
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto dto)
    {
        var updatedProduct = await productService.UpdateProductAsync(id, dto);
        return updatedProduct == null ? NotFound() : Ok(updatedProduct);
    }

    /// <summary>
    ///     Delete a product (soft delete)
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        return await productService.DeleteProductAsync(id)
            ? NoContent()
            : NotFound();
    }

    /// <summary>
    ///     Restore a soft-deleted product
    /// </summary>
    [HttpPatch("{id:guid}/restore")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Restore(Guid id)
    {
        return await productService.RestoreProductAsync(id)
            ? NoContent()
            : NotFound();
    }

    /// <summary>
    ///     Update product stock (increase/decrease)
    /// </summary>
    [HttpPatch("{productId:guid}/stock")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStock(
        Guid productId,
        [FromBody] int quantityChange)
    {
        var success = await productService.UpdateProductStockAsync(
            productId, quantityChange);

        if (!success)
        {
            var product = await productService.GetProductByIdAsync(productId);
            return product == null
                ? NotFound()
                : BadRequest("Insufficient stock");
        }

        return NoContent();
    }
}