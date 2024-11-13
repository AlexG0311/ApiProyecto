using Microsoft.AspNetCore.Mvc;
using Api.Context;
using Api.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Dtos;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductoesController(AppDbContext context)
        {
            _context = context;
        }
        //

        /// GET: api/Productos /
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoCreateDto>>> GetProductos()
        {
            var productos = await _context.producto
                .Select(p => new ProductoCreateDto
                {
                    idProducto = p.idProducto,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stok = p.Stok,
                    Img = p.Img
                })
                .ToListAsync();

            return productos;
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoCreateDto>> GetProducto(int id)
        {
            var producto = await _context.producto.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            var productoDTO = new ProductoCreateDto
            {
                idProducto = producto.idProducto,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stok = producto.Stok,
                Img = producto.Img
            };

            return productoDTO;
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<ProductoCreateDto>> PostProducto(ProductoCreateDto productoDTO)
        {
            var producto = new Producto
            {
                Nombre = productoDTO.Nombre,
                Descripcion = productoDTO.Descripcion,
                Precio = productoDTO.Precio,
                Stok = productoDTO.Stok,
                Img = productoDTO.Img
            };

            _context.producto.Add(producto);
            await _context.SaveChangesAsync();

            productoDTO.idProducto = producto.idProducto;

            return CreatedAtAction(nameof(GetProducto), new { id = productoDTO.idProducto }, productoDTO);
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, ProductoCreateDto productoDTO)
        {
            if (id != productoDTO.idProducto)
            {
                return BadRequest();
            }

            var producto = await _context.producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            producto.Nombre = productoDTO.Nombre;
            producto.Descripcion = productoDTO.Descripcion;
            producto.Precio = productoDTO.Precio;
            producto.Stok = productoDTO.Stok;
            producto.Img = productoDTO.Img;

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.producto.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return _context.producto.Any(e => e.idProducto == id);
        }
    }
}
