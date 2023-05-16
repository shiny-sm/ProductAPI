using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/Products/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(long id)
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            var product = await _context.TodoItems.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(long id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.TodoItems.Add(product);
            await _context.SaveChangesAsync();

            //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            //if (_context.TodoItems == null)
            //{
            //    return Problem("Entity set 'ProductContext.TodoItems'  is null.");
            //}
            //  _context.TodoItems.Add(product);
            //  await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            if (_context.TodoItems == null)
            {
                return NotFound();
            }
            var product = await _context.TodoItems.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(long id)
        {
            return (_context.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
