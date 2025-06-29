using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APINeoAlexandria.Utils;
using APINeoAlexandria.Data;
using APINeoAlexandria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace APINeoAlexandria.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly TpFinalProgramacionContext _context;

        public AutoresController(TpFinalProgramacionContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "ObtenerTodosAutores")]
        [AllowAnonymous]    
        public async Task<IActionResult> ObtenerTodosAutores()
        {
            try
            {
                var listaAutores = await _context.Autores.ToListAsync();
                return Ok(listaAutores);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerAutoresPorId/{IdAutor:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerAutoresPorId([FromRoute(Name = "IdAutor")] int id)
        {
            try
            {
                var item = await _context.Autores.FirstOrDefaultAsync(x => x.IdAutor == id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> AgregarAutor([FromBody] Autores autores)
        {
            try
            {
                var autor = new Autores()
                {
                  NombreAutor = autores.NombreAutor,
                  Biografia = autores.Biografia,
                  AnioFallecimiento = autores.AnioFallecimiento,
                  AnioNacimiento = autores.AnioNacimiento
                };
                    await _context.Autores.AddAsync(autor);
                    await _context.SaveChangesAsync();

                     return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{IdAutor:int}")]
        [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> EliminarAutores([FromRoute] int IdAutor)
        {
            try
            {
                var autorExistente = await _context.Autores.FindAsync(IdAutor);

                if (autorExistente != null)
                {
                    _context.Autores.Remove(autorExistente);
                    await _context.SaveChangesAsync();
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{IdAutor:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ModificarAutor([FromBody] Autores autor, [FromRoute] int IdAutor)
        {
            try
            {
                var autorExistente = await _context.Autores.FindAsync(IdAutor);

                if (autorExistente != null)
                {
                    if (autorExistente != null)
                    {
                        if (!string.IsNullOrEmpty(autor.NombreAutor)) autorExistente.NombreAutor = autor.NombreAutor;
                         if (!string.IsNullOrEmpty(autor.Biografia)) autorExistente.Biografia = autor.Biografia;
                        if (autor.AnioNacimiento != null) autorExistente.AnioNacimiento = autor.AnioNacimiento;
                        if (autor.AnioFallecimiento == null) autorExistente.AnioFallecimiento = autor.AnioFallecimiento;

                        _context.Autores.Update(autorExistente);
                        await _context.SaveChangesAsync();
                    }


                    _context.Autores.Update(autorExistente);
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