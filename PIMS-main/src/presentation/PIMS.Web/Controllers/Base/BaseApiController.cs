using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PIMS.Web.Common.Http;

namespace PIMS.Web.Controllers.Base
{
    /// <summary>
    /// Базовый API-контроллер.
    /// </summary>
    [ApiController]  
    public class BaseApiController: ControllerBase
    {
        /// <summary>
        /// Проблемы <see cref="IActionResult"/>.
        /// </summary>
        /// <param name="errors">Ошибки.</param>
        /// <returns>Возвращение значения результата действия (IActionResult).</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.Count == 0)
            {
                return Problem();
            }
            if (errors.All(m => m.Type == ErrorType.Validation ||
            m.Type == ErrorType.Conflict))
            {
                return ValidationProblem(errors);
            }
            HttpContext.Items[HttpContextItemKeys.Errors] = errors;
            return Problem(errors[0]);
        }

        /// <summary>
        /// Проблемы <see cref="IActionResult"/>.
        /// </summary>
        /// <param name="error">Ошибка.</param>
        /// <returns>Возвращение значения результата действия (IActionResult).</returns>
        private IActionResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
            return Problem(statusCode: statusCode, title: error.Description);
        }

        /// <summary>
        /// Проблема с валидацией.
        /// </summary>
        /// <param name="errors">Ошибки.</param>
        /// <returns>Возвращение значения результата действия (IActionResult).</returns>
        private IActionResult ValidationProblem(List<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();
            errors.ForEach(e => modelStateDictionary.AddModelError(e.Code, e.Description));
            return ValidationProblem(modelStateDictionary);
        }
    }
}
