using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APINeoAlexandria.Models;
using APINeoAlexandria.Models.DTO;
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
        public async Task<IActionResult> BorrarCarrito(int IdCarrito)
        {
            if (_context.Carritos == null)
            {
                return NotFound();
            }

            try
            {
                var detalles = _context.DetalleCarritos.Where(d => d.IdCarrito == IdCarrito).ToList();

                if (detalles.Any())
                {
                    _context.DetalleCarritos.RemoveRange(detalles);
                    await _context.SaveChangesAsync();
                }

                var carrito = await _context.Carritos.FindAsync(IdCarrito);
                if (carrito == null)
                {
                    return NotFound();
                }

                _context.Carritos.Remove(carrito);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar carrito: {ex.Message} | Inner: {ex.InnerException?.Message}");
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

        [HttpPost("CrearConDetalle")]
        [AllowAnonymous]
        public async Task<IActionResult> CrearCarritoConDetalle([FromBody] CarritoConDetalleDTO dto)
        {
            try
            {
                var nuevoCarrito = new Carrito
                {
                    IdUsuario = dto.IdUsuario,
                    PrecioTotalCarrito = dto.PrecioTotalDetalleCarrito,
                    FechaCreacion = DateTime.Now,
                    Descripcion = $"Compra del día {DateTime.Now:dd/MM/yyyy}",
                    Estado = true
                };

                _context.Carritos.Add(nuevoCarrito);
                await _context.SaveChangesAsync();

                var nuevoDetalle = new DetalleCarrito
                {
                    IdCarrito = nuevoCarrito.IdCarrito,
                    IdLibro = dto.IdLibro,
                    PrecioTotalDetalleCarrito = dto.PrecioTotalDetalleCarrito,
                    FechaFactura = DateTime.Now,
                    FechaCreacionFactura = DateTime.Now,
                    DetalleFactura = $"Libro comprado el {DateTime.Now:dd/MM/yyyy}"
                };

                _context.DetalleCarritos.Add(nuevoDetalle);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Carrito = nuevoCarrito,
                    Detalle = nuevoDetalle
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = "Error al crear carrito con detalle", detalle = ex.Message });
            }
        }
    }
}