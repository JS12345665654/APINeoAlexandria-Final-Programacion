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
    public class CategoriasController : ControllerBase
    {
        private readonly TpFinalProgramacionContext _context;

        public CategoriasController(TpFinalProgramacionContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "ObtenerTodasCategorias")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerTodasCategorias()
        {
            try
            {
                var listacategorias = await _context.Categoria.ToListAsync();
                return Ok(listacategorias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerCategoriasPorId/{IdCategoria:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerCategoriasPorId([FromRoute(Name = "IdCategoria")] int id)
        {
            try
            {
                var item = await _context.Categoria.FirstOrDefaultAsync(x => x.IdCategoria == id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> AgregarCategoria([FromBody] Categoria categorias)
        {
            try
            {
                var categoria = new Categoria()
                {
                    NombreCategoria = categorias.NombreCategoria,
                    DescripcionCategoria = categorias.DescripcionCategoria
                };
                await _context.Categoria.AddAsync(categoria);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{IdCategoria:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EliminarCategoria([FromRoute] int IdCategoria)
        {
            try
            {
                var categoriaExistente = await _context.Categoria.FindAsync(IdCategoria);

                if (categoriaExistente != null)
                {
                    _context.Categoria.Remove(categoriaExistente);
                    await _context.SaveChangesAsync();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{IdCategoria:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ModificarCategoria([FromBody] Categoria categoria , [FromRoute] int IdCategoria)
        {
            try
            {
                var categoriaExistente = await _context.Categoria.FindAsync(IdCategoria);

                if (categoriaExistente != null)
                {
                    if (categoriaExistente != null)
                    {
                        if (!string.IsNullOrEmpty(categoria.NombreCategoria)) categoriaExistente.NombreCategoria = categoria.NombreCategoria;
                        if (!string.IsNullOrEmpty(categoria.DescripcionCategoria)) categoriaExistente.DescripcionCategoria = categoria.DescripcionCategoria;
                        
                        _context.Categoria.Update(categoriaExistente);
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