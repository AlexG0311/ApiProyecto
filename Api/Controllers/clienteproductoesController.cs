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
    public class clienteproductoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public clienteproductoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/clienteproductoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<clienteproducto>>> Getclienteproducto()
        {
            return await _context.clienteproducto.ToListAsync();
        }

        // GET: api/clienteproductoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<clienteproducto>> Getclienteproducto(int id)
        {
            var clienteproducto = await _context.clienteproducto.FindAsync(id);

            if (clienteproducto == null)
            {
                return NotFound();
            }

            return clienteproducto;
        }

        // PUT: api/clienteproductoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putclienteproducto(int id, clienteproducto clienteproducto)
        {
            if (id != clienteproducto.cliente_idCliente)
            {
                return BadRequest();
            }

            _context.Entry(clienteproducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clienteproductoExists(id))
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

        // POST: api/clienteproductoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<clienteproducto>> Postclienteproducto(clienteproducto clienteproducto)
        {
            _context.clienteproducto.Add(clienteproducto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (clienteproductoExists(clienteproducto.cliente_idCliente))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getclienteproducto", new { id = clienteproducto.cliente_idCliente }, clienteproducto);
        }

        // DELETE: api/clienteproductoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteclienteproducto(int id)
        {
            var clienteproducto = await _context.clienteproducto.FindAsync(id);
            if (clienteproducto == null)
            {
                return NotFound();
            }

            _context.clienteproducto.Remove(clienteproducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool clienteproductoExists(int id)
        {
            return _context.clienteproducto.Any(e => e.cliente_idCliente == id);
        }
    }
}
