using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APINeoAlexandria.Models;

public partial class DetalleCarrito
{
    [Key]
    public int IdDetalleCarrito { get; set; }

    public decimal PrecioTotalDetalleCarrito { get; set; }

    [ForeignKey("Carrito")]
    public int IdCarrito { get; set; }

    public DateTime? FechaFactura { get; set; }

    [ForeignKey("Libro")]
    public int IdLibro { get; set; }

    public string? DetalleFactura { get; set; }

    public DateTime FechaCreacionFactura { get; set; }
}
