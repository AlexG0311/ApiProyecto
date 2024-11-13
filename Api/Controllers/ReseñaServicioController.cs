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
    public class ReseñaServicioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReseñaServicioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ReseñaServicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReseñaServicio>>> Getreseñaservicio()
        {
            return await _context.reseñaservicio.ToListAsync();
        }

        // GET: api/ReseñaServicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReseñaServicio>> GetReseñaServicio(int id)
        {
            var reseñaServicio = await _context.reseñaservicio.FindAsync(id);

            if (reseñaServicio == null)
            {
                return NotFound();
            }

            return reseñaServicio;
        }

        // PUT: api/ReseñaServicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReseñaServicio(int id, ReseñaServicio reseñaServicio)
        {
            if (id != reseñaServicio.idReseñaServicio)
            {
                return BadRequest();
            }

            _context.Entry(reseñaServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReseñaServicioExists(id))
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

        // POST: api/ReseñaServicio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReseñaServicio>> PostReseñaServicio(ReseñaServicio reseñaServicio)
        {
            _context.reseñaservicio.Add(reseñaServicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReseñaServicio", new { id = reseñaServicio.idReseñaServicio }, reseñaServicio);
        }

        // DELETE: api/ReseñaServicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReseñaServicio(int id)
        {
            var reseñaServicio = await _context.reseñaservicio.FindAsync(id);
            if (reseñaServicio == null)
            {
                return NotFound();
            }

            _context.reseñaservicio.Remove(reseñaServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReseñaServicioExists(int id)
        {
            return _context.reseñaservicio.Any(e => e.idReseñaServicio == id);
        }
    }
}
