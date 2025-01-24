using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace APINeoAlexandria.Models;

public partial class Notas
{
    public int IdNota { get; set; }

    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }

    [ForeignKey("Libros")]
    public int IdLibro { get; set; }

    public string? TextoNota { get; set; }
}
