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
    public class EspecialidadesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EspecialidadesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Especialidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Especialidades>>> Getespecialidades()
        {
            return await _context.especialidades.ToListAsync();
        }

        // GET: api/Especialidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Especialidades>> GetEspecialidades(int id)
        {
            var especialidades = await _context.especialidades.FindAsync(id);

            if (especialidades == null)
            {
                return NotFound();
            }

            return especialidades;
        }

        // PUT: api/Especialidades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidades(int id, Especialidades especialidades)
        {
            if (id != especialidades.idEspecialidades)
            {
                return BadRequest();
            }

            _context.Entry(especialidades).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspecialidadesExists(id))
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

        // POST: api/Especialidades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Especialidades>> PostEspecialidades(Especialidades especialidades)
        {
            _context.especialidades.Add(especialidades);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEspecialidades", new { id = especialidades.idEspecialidades }, especialidades);
        }

        // DELETE: api/Especialidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidades(int id)
        {
            var especialidades = await _context.especialidades.FindAsync(id);
            if (especialidades == null)
            {
                return NotFound();
            }

            _context.especialidades.Remove(especialidades);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EspecialidadesExists(int id)
        {
            return _context.especialidades.Any(e => e.idEspecialidades == id);
        }
    }
}
