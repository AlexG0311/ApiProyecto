using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Context;
using Api.Model;
using Api.Dtos;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Getusuario()
        {
            return await _context.usuario.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.idUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioCreateDto usuarioDto)
        {
            // Crear un nuevo Usuario a partir del DTO
            var usuario = new Usuario
            {
                Nombre = usuarioDto.Nombre,
                Apellidos = usuarioDto.Apellidos,
                Correo = usuarioDto.Correo,
                Contrasena = usuarioDto.Contrasena,
                Telefono = usuarioDto.Telefono
            };

            // Añadir el usuario a la base de datos
            _context.usuario.Add(usuario);
            await _context.SaveChangesAsync();

            // Crear una entrada en la tabla Cliente automáticamente
            var cliente = new Cliente
            {
                Usuario_idUsuario = usuario.idUsuario  // Asocia el cliente con el id del usuario recién creado
            };
            _context.cliente.Add(cliente);
            await _context.SaveChangesAsync();

            // Retornar el usuario creado con la relación de cliente creada
            return CreatedAtAction("GetUsuario", new { id = usuario.idUsuario }, usuario);
        }



        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.usuario.Any(e => e.idUsuario == id);
        }
    }
}
