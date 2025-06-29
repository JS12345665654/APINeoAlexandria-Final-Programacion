using System.ComponentModel.DataAnnotations;

namespace APINeoAlexandria.Models.DTO
{
    public class UsuarioDTO
    {
        [Key]
        public int IdUsuario { get; set; }

        public string? Nombre { get; set; }

        public string? Email { get; set; }

        public string? Contrasenia { get; set; }
        public string? Imagen { get; set; }
        public string? CategoriaPreferida { get; set; }
        public Rol Rol { get; set; }

    }
}
