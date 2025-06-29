using System.ComponentModel.DataAnnotations;

namespace APINeoAlexandria.Models.DTO
{
    public class LoginResponseDTO
    {
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? IdRol { get; set; }
        public bool Autenticado { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
    }
}
