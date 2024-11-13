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
    public class ReseñaProductoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReseñaProductoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ReseñaProducto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReseñaProducto>>> Getreseñasproducto()
        {
            return await _context.reseñasproducto.ToListAsync();
        }

        // GET: api/ReseñaProducto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReseñaProducto>> GetReseñaProducto(int id)
        {
            var reseñaProducto = await _context.reseñasproducto.FindAsync(id);

            if (reseñaProducto == null)
            {
                return NotFound();
            }

            return reseñaProducto;
        }

        // PUT: api/ReseñaProducto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReseñaProducto(int id, ReseñaProducto reseñaProducto)
        {
            if (id != reseñaProducto.idReseñaProducto)
            {
                return BadRequest();
            }

            _context.Entry(reseñaProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReseñaProductoExists(id))
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

        // POST: api/ReseñaProducto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReseñaProducto>> PostReseñaProducto(ReseñaProducto reseñaProducto)
        {
            _context.reseñasproducto.Add(reseñaProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReseñaProducto", new { id = reseñaProducto.idReseñaProducto }, reseñaProducto);
        }

        // DELETE: api/ReseñaProducto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReseñaProducto(int id)
        {
            var reseñaProducto = await _context.reseñasproducto.FindAsync(id);
            if (reseñaProducto == null)
            {
                return NotFound();
            }

            _context.reseñasproducto.Remove(reseñaProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReseñaProductoExists(int id)
        {
            return _context.reseñasproducto.Any(e => e.idReseñaProducto == id);
        }
    }
}
