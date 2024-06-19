using Microsoft.AspNetCore.Mvc;
using PIMS.Web.Controllers.Base;

namespace PIMS.Web.Controllers.v1
{
    /// <summary>
    /// Контроллер проверки доступа.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class AccessTestController:BaseApiController
    {
        /// <summary>
        /// Тестовая страница.
        /// </summary>
        /// <param name="TestRequestMessage">Сообщение с запросом на тестирование.</param>
        /// <returns>Возвращение значения результата действия (IActionResult).</returns>
        [HttpPost("testpage")]
        public IActionResult TestPage(string TestRequestMessage)
        {
            return Ok($"Get message: {TestRequestMessage} at {DateTime.UtcNow.ToString()}");
        }
    }
}
