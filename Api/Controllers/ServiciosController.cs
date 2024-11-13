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
    public class ServiciosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServiciosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicioCreateDto>>> GetServicios()
        {
            var servicios = await _context.servicio
                .Select(s => new ServicioCreateDto
                {
                    idServicio = s.idServicio,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    Precio = s.Precio,
                    Duracion = s.Duracion,
                    Img = s.Img
                })
                .ToListAsync();

            return servicios;
        }

        // GET: api/Servicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicioCreateDto>> GetServicio(int id)
        {
            var servicio = await _context.servicio.FindAsync(id);

            if (servicio == null)
            {
                return NotFound();
            }

            var servicioDTO = new ServicioCreateDto
            {
                idServicio = servicio.idServicio,
                Nombre = servicio.Nombre,
                Descripcion = servicio.Descripcion,
                Precio = servicio.Precio,
                Duracion = servicio.Duracion,
                Img = servicio.Img
            };

            return servicioDTO;
        }

        // POST: api/Servicios
        [HttpPost]
        public async Task<ActionResult<ServicioCreateDto>> PostServicio(ServicioCreateDto servicioDTO)
        {
            var servicio = new Servicio
            {
                Nombre = servicioDTO.Nombre,
                Descripcion = servicioDTO.Descripcion,
                Precio = servicioDTO.Precio,
                Duracion = servicioDTO.Duracion,
                Img = servicioDTO.Img
            };

            _context.servicio.Add(servicio);
            await _context.SaveChangesAsync();

            servicioDTO.idServicio = servicio.idServicio;

            return CreatedAtAction(nameof(GetServicio), new { id = servicioDTO.idServicio }, servicioDTO);
        }

        // PUT: api/Servicios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicio(int id, ServicioCreateDto servicioDTO)
        {
            if (id != servicioDTO.idServicio)
            {
                return BadRequest();
            }

            var servicio = await _context.servicio.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }

            servicio.Nombre = servicioDTO.Nombre;
            servicio.Descripcion = servicioDTO.Descripcion;
            servicio.Precio = servicioDTO.Precio;
            servicio.Duracion = servicioDTO.Duracion;
            servicio.Img = servicioDTO.Img;

            _context.Entry(servicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicioExists(id))
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

        // DELETE: api/Servicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            var servicio = await _context.servicio.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }

            _context.servicio.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicioExists(int id)
        {
            return _context.servicio.Any(e => e.idServicio == id);
        }
    }
}
