using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APINeoAlexandria.Models;

public partial class Autores
{
    [Key]
    public int IdAutor { get; set; }

    public string NombreAutor { get; set; }

    public string Biografia { get; set; }

    public DateTime AnioNacimiento { get; set; }

    public DateTime? AnioFallecimiento { get; set; }
}
