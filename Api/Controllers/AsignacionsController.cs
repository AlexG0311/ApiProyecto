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
    public class AsignacionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AsignacionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Asignacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignacion>>> Getasignacion()
        {
            return await _context.asignacion.ToListAsync();
        }

        // GET: api/Asignacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignacion>> GetAsignacion(int id)
        {
            var asignacion = await _context.asignacion.FindAsync(id);

            if (asignacion == null)
            {
                return NotFound();
            }

            return asignacion;
        }

        // PUT: api/Asignacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignacion(int id, Asignacion asignacion)
        {
            if (id != asignacion.idAsignacion)
            {
                return BadRequest();
            }

            _context.Entry(asignacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignacionExists(id))
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

        // POST: api/Asignacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asignacion>> PostAsignacion(Asignacion asignacion)
        {
            _context.asignacion.Add(asignacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignacion", new { id = asignacion.idAsignacion }, asignacion);
        }

        // DELETE: api/Asignacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignacion(int id)
        {
            var asignacion = await _context.asignacion.FindAsync(id);
            if (asignacion == null)
            {
                return NotFound();
            }

            _context.asignacion.Remove(asignacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsignacionExists(int id)
        {
            return _context.asignacion.Any(e => e.idAsignacion == id);
        }
    }
}
