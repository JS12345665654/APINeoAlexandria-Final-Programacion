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
    public class NotasController : ControllerBase
    {
        private readonly TpFinalProgramacionContext _context;

        public NotasController(TpFinalProgramacionContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "ObtenerTodasNotas")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerTodasNotas()
        {
            try
            {
                var listanotas = await _context.Notas.ToListAsync();
                return Ok(listanotas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerNotasPorId/{IdNota:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerNotasPorId([FromRoute(Name = "IdNota")] int id)
        {
            try
            {
                var item = await _context.Notas.FirstOrDefaultAsync(x => x.IdNota == id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> AgregarNota([FromBody] Notas notas)
        {
            try
            {
                var nota = new Notas()
                {
                    IdUsuario = notas.IdUsuario,
                    IdLibro = notas.IdLibro,
                    TextoNota = notas.TextoNota, // Puede ser una nota, reflexión o fragmento de lo que leyeron que les gustó
                };

                await _context.Notas.AddAsync(notas);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{IdNota:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EliminarNota([FromRoute] int IdNota)
        {
            try
            {
                var notaExistente = await _context.Notas.FindAsync(IdNota);

                if (notaExistente != null)
                {
                    _context.Notas.Remove(notaExistente);
                    await _context.SaveChangesAsync();
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{IdNota:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ModificarNota([FromBody] Notas notas, [FromRoute] int IdNota)
        {
            try
            {
                var notaExistente = await _context.Notas.FindAsync(IdNota);

                if (notaExistente != null)
                {
                    if (notaExistente != null)
                    {
                        if (!string.IsNullOrEmpty(notas.TextoNota)) notaExistente.TextoNota = notas.TextoNota;
                        if (notas.IdUsuario != null) notaExistente.IdUsuario = notas.IdUsuario;
                        if (notas.IdLibro != null) notaExistente.IdLibro = notas.IdLibro;

                        _context.Notas.Update(notaExistente);
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
    }
}