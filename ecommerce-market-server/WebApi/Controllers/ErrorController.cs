using Microsoft.AspNetCore.Mvc;
using WebApi.Errors;

namespace WebApi.Controllers
{
    [Route("errors")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Maneja los errores de la API y devuelve una respuesta JSON con el código de error.
        /// </summary>
        /// <param name="code">El código de error.</param>
        /// <returns>Una respuesta JSON con el código de error.</returns>
        public IActionResult Error(int code)
        {
            return new ObjectResult(new CodeErrorResponse(code));
        }
    }
}