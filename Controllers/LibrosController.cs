using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APINeoAlexandria.Utils;
using APINeoAlexandria.Data;
using APINeoAlexandria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace ApiStore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibrosController : ControllerBase
{
    private readonly TpFinalProgramacionContext _context;

    public LibrosController(TpFinalProgramacionContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "ObtenerTodosLibros")]
    [AllowAnonymous]
    public async Task<IActionResult> ObtenerTodosLibros()
    {
        try
        {
            var listalibros = await _context.Libros.ToListAsync();
            return Ok(listalibros);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ObtenerLibroPorId/{IdLibro:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> ObtenerLibroPorId([FromRoute(Name = "IdLibro")] int id)
    {
        try
        {
            var item = await _context.Libros.FirstOrDefaultAsync(x => x.IdLibro == id);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost()]
    [AllowAnonymous]
    public async Task<IActionResult> CrearLibro([FromBody] Libros libro)
    {
        try
        {
            libro.Imagen = "Imagen/";
            await _context.Libros.AddAsync(libro);
            var result = await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("CrearConImagen")]
    [AllowAnonymous]
    public async Task<IActionResult> CrearConImagen([FromForm] Libros crearLibro)
    {
        try
        {

            var libro = new Libros()
            {
                IdAutor = crearLibro.IdAutor,
                IdCategoria = crearLibro.IdCategoria,
                Nombre = crearLibro.Nombre,
                Precio = crearLibro.Precio,
                AniodePublicacion = crearLibro.AniodePublicacion,
                Descripcion = crearLibro.Descripcion,
                Stock = crearLibro.Stock,
            };

            await _context.Libros.AddAsync(libro);
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{IdLibro:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> EliminarLibro([FromRoute] int IdLibro)
    {
        try
        {
            var libroExistente = await _context.Libros.FindAsync(IdLibro);

            if (libroExistente != null)
            {
                _context.Libros.Remove(libroExistente);
                await _context.SaveChangesAsync();
            }


            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{IdLibro:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ModificarLibro([FromBody] Libros libro, [FromRoute] int IdLibro)
    {
        try
        {
            var libroExistente = await _context.Libros.FindAsync(IdLibro);

            if (libroExistente != null)
            {
                if (libroExistente != null)
                {
                    if (!string.IsNullOrEmpty(libro.Descripcion)) libroExistente.Descripcion = libro.Descripcion;
                    if (libro.IdAutor != null) libroExistente.IdAutor = libro.IdAutor;
                    if (libro.IdCategoria != null) libroExistente.IdCategoria = libro.IdCategoria;
                    if (!string.IsNullOrEmpty(libro.Nombre)) libroExistente.Nombre = libro.Nombre;
                    if (libro.Precio != null) libroExistente.Precio = libro.Precio;
                    if (libro.Stock != null) libroExistente.Stock = libro.Stock;

                    _context.Libros.Update(libroExistente);
                    await _context.SaveChangesAsync();
                }


                _context.Libros.Update(libroExistente);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("GuardarImagen")]
    public async Task<IActionResult> GuardarImagen([FromForm] UploadFileApi archivo)
    {
        var ruta = string.Empty;

        if (archivo.Archivo.Length > 0)
        {
            var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.Archivo.FileName);
            ruta = $"Images/{nombreArchivo}";
            using (var stream = new FileStream(ruta, FileMode.Create))
            {
                try
                {
                    await archivo.Archivo.CopyToAsync(stream);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error al grabar archivo: " + ex.Message);
                }
            }
        }
        return Ok();
    }
}
