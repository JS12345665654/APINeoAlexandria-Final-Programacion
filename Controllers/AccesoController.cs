using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using APINeoAlexandria.Utils;
using APINeoAlexandria.Models;
using APINeoAlexandria.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using APINeoAlexandria.Data;

namespace APINeoAlexandria.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly TpFinalProgramacionContext _TpFinalProgramacionContext;
        private readonly Encriptar _Encriptar;
        public AccesoController(TpFinalProgramacionContext tpFinalProgramacionContext, Encriptar encriptar)
        {
            _TpFinalProgramacionContext = tpFinalProgramacionContext;
            _Encriptar = encriptar;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginResponseDTO objeto)
        {
            var usuarioEncontrado = await _TpFinalProgramacionContext.Usuarios
                                    .Where(U =>
                                        U.Email == objeto.Email &&
                                        U.Contrasenia == _Encriptar.encriptarSHA256(objeto.Contrasenia)
                                    ).FirstOrDefaultAsync();
            
            if(usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK, new {isSuccess=false, token = ""});
            else
                return StatusCode(StatusCodes.Status200OK, new {isSuccess = true, token = _Encriptar.generarJWT(usuarioEncontrado)});
        }
    }
}
