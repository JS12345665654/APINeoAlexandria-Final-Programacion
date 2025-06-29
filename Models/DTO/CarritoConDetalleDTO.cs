using System.ComponentModel.DataAnnotations;

namespace APINeoAlexandria.Models.DTO
{
    public class CarritoConDetalleDTO
    {
        public int IdUsuario { get; set; }
        public int IdLibro { get; set; }
        public decimal PrecioTotalDetalleCarrito { get; set; }
    }
}
