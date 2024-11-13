using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Context;
using Api.Model;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DescuentoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DescuentoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Descuentoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Descuento>>> Getdescuento()
        {
            return await _context.descuento.ToListAsync();
        }

        // GET: api/Descuentoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Descuento>> GetDescuento(int id)
        {
            var descuento = await _context.descuento.FindAsync(id);

            if (descuento == null)
            {
                return NotFound();
            }

            return descuento;
        }

        // PUT: api/Descuentoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDescuento(int id, Descuento descuento)
        {
            if (id != descuento.producto_idProducto)
            {
                return BadRequest();
            }

            _context.Entry(descuento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DescuentoExists(id))
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

        // POST: api/Descuentoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Descuento>> PostDescuento(Descuento descuento)
        {
            _context.descuento.Add(descuento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DescuentoExists(descuento.producto_idProducto))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDescuento", new { id = descuento.producto_idProducto }, descuento);
        }

        // DELETE: api/Descuentoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDescuento(int id)
        {
            var descuento = await _context.descuento.FindAsync(id);
            if (descuento == null)
            {
                return NotFound();
            }

            _context.descuento.Remove(descuento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DescuentoExists(int id)
        {
            return _context.descuento.Any(e => e.producto_idProducto == id);
        }
    }
}
