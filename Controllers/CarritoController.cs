using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APINeoAlexandria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using APINeoAlexandria.Data;


namespace APINeoAlexandria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly TpFinalProgramacionContext _context;

        public CarritoController(TpFinalProgramacionContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "ObtenerTodosCarritos")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerTodosCarritos()
        {
            try
            {
                var listacarritos = await _context.Carritos.ToListAsync();
                return Ok(listacarritos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerPorId/{IdCarrito:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerCarritosPorId([FromRoute(Name = "IdCarrito")] int id)
        {
            try
            {
                var item = await _context.Carritos.FirstOrDefaultAsync(x => x.IdCarrito == id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> CrearCarritos([FromBody] Carrito carrito)
        {
            try
            {
                await _context.Carritos.AddAsync(carrito);
                var result = await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{IdCarrito:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> BorrarCarrito([FromRoute] int IdCarrito)
        {
            try
            {
                var carritoExistente = await _context.Carritos.FindAsync(IdCarrito);

                if (carritoExistente != null)
                {
                    _context.Carritos.Remove(carritoExistente);
                    await _context.SaveChangesAsync();
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{IdCarrito:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ModificarCarrito([FromBody] Carrito carrito, [FromRoute] int IdCarrito)
        {
            try
            {
                var carritoExistente = await _context.Carritos.FindAsync(IdCarrito);

                if (carritoExistente != null)
                {
                    if (carrito.FechaCreacion != null) carritoExistente.FechaCreacion = carrito.FechaCreacion;
                    if (carrito.IdUsuario != null) carritoExistente.IdUsuario = carrito.IdUsuario;
                    if (carrito.Descripcion != null) carritoExistente.Descripcion = carrito.Descripcion;
                    if (carrito.Estado != null) carritoExistente.Estado = carrito.Estado;
                    if (carrito.PrecioTotalCarrito != null) carritoExistente.PrecioTotalCarrito = carrito.PrecioTotalCarrito;

                    _context.Carritos.Update(carritoExistente);
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