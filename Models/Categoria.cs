using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APINeoAlexandria.Models;

public partial class Categoria
{
    [Key]
    public int IdCategoria { get; set; }

    public string? NombreCategoria { get; set; }

    public string? DescripcionCategoria { get; set; }
}
