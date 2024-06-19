using Microsoft.AspNetCore.Mvc;
using PIMS.Web.Controllers.Base;

namespace PIMS.Web.Controllers
{

    /// <summary>
    /// Контроллер ошибок.
    /// </summary>
    public class ErrorsController : ControllerBase
    {
        /// <summary>
        /// Ошибки <see cref="IActionResult"/>.
        /// </summary>
        /// <returns>Возвращение значения результата действия (IActionResult).</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult Error()
        {
            return Problem();
        }

    }
}
