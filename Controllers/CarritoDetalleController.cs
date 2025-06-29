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
    public class CarritoDetalleController : ControllerBase
    {
        private readonly TpFinalProgramacionContext _context;

        public CarritoDetalleController(TpFinalProgramacionContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "ObtenerDetalleCarritos")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerDetalleCarritos()
        {
            try
            {
                var listadetallecarrito = await _context.DetalleCarritos.ToListAsync();
                return Ok(listadetallecarrito);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerPorId/{IdDetalleCarrito:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerDetalleCarritoPorId([FromRoute(Name = "IdDetalleCarrito")] int id)
        {
            try
            {
                var item = await _context.DetalleCarritos.FirstOrDefaultAsync(x => x.IdDetalleCarrito == id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> CrearDetalleCarrito([FromBody] DetalleCarrito detallecarrito)
        {
            try
            {
                var detalleCarrito = new DetalleCarrito()
                {
                    IdDetalleCarrito = detallecarrito.IdDetalleCarrito,
                    PrecioTotalDetalleCarrito = detallecarrito.PrecioTotalDetalleCarrito,
                    IdCarrito = detallecarrito.IdCarrito,
                    FechaFactura = detallecarrito.FechaFactura,
                    IdLibro = detallecarrito.IdLibro,
                    DetalleFactura = detallecarrito.DetalleFactura,
                    FechaCreacionFactura = detallecarrito.FechaCreacionFactura,
                };
                await _context.DetalleCarritos.AddAsync(detallecarrito);
                var result = await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{IdDetalleCarrito:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> BorrarDetalleCarrito([FromRoute] int IdDetalleCarrito)
        {
            try
            {
                var carritodetalleExistente = await _context.DetalleCarritos.FindAsync(IdDetalleCarrito);

                if (carritodetalleExistente != null)
                {
                    _context.DetalleCarritos.Remove(carritodetalleExistente);
                    await _context.SaveChangesAsync();
                }


                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{IdDetalleCarrito:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> ModificarDetalleCarrito([FromBody] DetalleCarrito detallecarrito, [FromRoute] int IdDetalleCarrito)
        {
            try
            {
                var carritodetalleExistente = await _context.DetalleCarritos.FindAsync(IdDetalleCarrito);

                if (carritodetalleExistente != null)
                {
                    if (detallecarrito.FechaCreacionFactura != null) carritodetalleExistente.FechaCreacionFactura = detallecarrito.FechaCreacionFactura;
                    if (detallecarrito.FechaFactura != null) carritodetalleExistente.FechaFactura = detallecarrito.FechaFactura;
                    if (detallecarrito.IdCarrito != null) carritodetalleExistente.IdCarrito = detallecarrito.IdCarrito;
                    if (detallecarrito.PrecioTotalDetalleCarrito != null) carritodetalleExistente.PrecioTotalDetalleCarrito = detallecarrito.PrecioTotalDetalleCarrito;
                    if (detallecarrito.DetalleFactura != null) carritodetalleExistente.DetalleFactura = detallecarrito.DetalleFactura;

                    _context.DetalleCarritos.Update(carritodetalleExistente);
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
