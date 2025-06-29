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
    public class ValoracionUsuariosController : ControllerBase
    {
        private readonly TpFinalProgramacionContext _context;

        public ValoracionUsuariosController(TpFinalProgramacionContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "ObtenerTodasValoraciones")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerTodasValoraciones()
        {
            try
            {
                var listavaloraciones = await _context.ValoraciondeUsuarios.ToListAsync();
                return Ok(listavaloraciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerValoracionesPorId/{IdValoracion:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerValoracionesPorId([FromRoute(Name = "IdValoracion")] int id)
        {
            try
            {
                var item = await _context.ValoraciondeUsuarios.FirstOrDefaultAsync(x => x.IdValoracion == id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> AgregarValoracion([FromBody] ValoraciondeUsuario valoraciondeUsuario)
        {
            try
            {
                var valoracion = new ValoraciondeUsuario()
                {
                    IdValoracion = valoraciondeUsuario.IdValoracion,
                    IdUsuario = valoraciondeUsuario.IdUsuario,
                    IdLibro = valoraciondeUsuario.IdLibro,
                    Valoracion = valoraciondeUsuario.Valoracion,
                    Puntuacion = valoraciondeUsuario.Puntuacion /// valor entre 1 y 5 depende de que valoracion queramos dar
                };

                await _context.ValoraciondeUsuarios.AddAsync(valoracion);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{IdValoracion:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EliminarValoracion([FromRoute] int IdValoracion)
        {
            try
            {
                var valoracionExistente = await _context.ValoraciondeUsuarios.FindAsync(IdValoracion);

                if (valoracionExistente != null)
                {
                    _context.ValoraciondeUsuarios.Remove(valoracionExistente);
                    await _context.SaveChangesAsync();
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{IdValoracion:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ModificarValoracion([FromBody] ValoraciondeUsuario valoraciondeU, [FromRoute] int IdValoracion)
        {
            try
            {
                var valoracionExistente = await _context.ValoraciondeUsuarios.FindAsync(IdValoracion);

                if (valoracionExistente != null)
                {
                    if (valoracionExistente != null)
                    {
                        if (!string.IsNullOrEmpty(valoraciondeU.Valoracion)) valoracionExistente.Valoracion = valoraciondeU.Valoracion;
                        if (valoraciondeU.IdUsuario != null) valoracionExistente.IdUsuario = valoraciondeU.IdUsuario;
                        if (valoraciondeU.IdLibro != null) valoracionExistente.IdLibro = valoraciondeU.IdLibro;
                        if (valoraciondeU.Puntuacion != null) valoracionExistente.Puntuacion = valoraciondeU.Puntuacion;

                        _context.ValoraciondeUsuarios.Update(valoracionExistente);
                        await _context.SaveChangesAsync();
                    }
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("PorLibro/{idLibro}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ValoraciondeUsuario>>> GetValoracionesPorLibro(int idLibro)
        {
            try
            {
                var valoraciones = await _context.ValoraciondeUsuarios
                    .Where(v => v.IdLibro == idLibro)
                    .ToListAsync();

                if (valoraciones == null || valoraciones.Count == 0)
                    return NotFound("No se encontraron valoraciones para este libro.");

                return Ok(valoraciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las valoraciones: {ex.Message}");
            }
        }

    }
}