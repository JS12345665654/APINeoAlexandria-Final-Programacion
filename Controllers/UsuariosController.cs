using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APINeoAlexandria.Utils;
using APINeoAlexandria.Models;
using APINeoAlexandria.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using APINeoAlexandria.Data;

namespace APINeoAlexandria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly TpFinalProgramacionContext _context;
        private readonly Encriptar _Encriptar;

        public UsuariosController(TpFinalProgramacionContext context, Encriptar encriptar)
        {
            _context = context;
            _Encriptar = encriptar;
        }

        [HttpGet(Name = "ObtenerTodosUsuarios")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerTodosUsuarios()
        {
            try
            {
                var listausuarios = await _context.Usuarios.ToListAsync();
                return Ok(listausuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerPorId/{IdUsuario:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerUsuariosPorId([FromRoute(Name = "IdUsuario")] int id)
        {
            try
            {
                var item = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> CrearUsuarios([FromBody] Usuarios usuarios)
        {
            try
            {
                var usuario = new Usuarios()
                {
                    Nombre = usuarios.Nombre,
                    Email = usuarios.Email,
                    Contrasenia = _Encriptar.encriptarSHA256(usuarios.Contrasenia),
                    CategoriaPreferida = usuarios.CategoriaPreferida,
                    Rol = usuarios.Rol,
                    Activo = usuarios.Activo
                };

                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{IdUsuario:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> BorrarUsuario([FromRoute] int IdUsuario)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(IdUsuario);

                if (usuarioExistente != null)
                {
                    _context.Usuarios.Remove(usuarioExistente);
                    await _context.SaveChangesAsync();
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{IdUsuario:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ModificarUsuario([FromBody] Usuarios usuarios, [FromRoute] int IdUsuario)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(IdUsuario);

                if (usuarioExistente != null)
                {
                    if (!string.IsNullOrEmpty(usuarios.Email)) usuarioExistente.Email = usuarios.Email;
                    if (!string.IsNullOrEmpty(usuarios.Nombre)) usuarioExistente.Nombre = usuarios.Nombre;
                    if (!string.IsNullOrEmpty(usuarios.Contrasenia)) usuarioExistente.Contrasenia = usuarios.Contrasenia;
                    if (!string.IsNullOrEmpty(usuarios.CategoriaPreferida)) usuarioExistente.CategoriaPreferida = usuarios.CategoriaPreferida;
                    if (usuarios.Rol != null) usuarioExistente.Rol = usuarios.Rol;

                    _context.Usuarios.Update(usuarioExistente);
                    await _context.SaveChangesAsync();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}