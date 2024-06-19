using Microsoft.AspNetCore.Mvc;
using PIMS.Web.Controllers.Base;

namespace PIMS.Web.Controllers.v1
{
    /// <summary>
    /// Домашний контроллер jwt.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class JwtHomeController:JwtBasedApiController
    {
        /// <summary>
        /// Профилирует <see cref="IActionResult"/>.
        /// </summary>
        /// <returns>Возвращение значения результата действия (IActionResult).</returns>
        [HttpGet("Profile")]
        public IActionResult Profile()
        {
            var t = HttpContext.User.Identity;

            return Ok(Array.Empty<string>());
        }
    }
}
