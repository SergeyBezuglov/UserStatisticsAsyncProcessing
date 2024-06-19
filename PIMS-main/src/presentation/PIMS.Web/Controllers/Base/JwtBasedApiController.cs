using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIMS.Domain.Common.Authentication.Configuration;

namespace PIMS.Web.Controllers.Base
{
    /// <summary>
    /// API-контроллер на основе jwt.
    /// </summary>
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtAuthenticationModuleOption.SchemeName)]
    public class JwtBasedApiController : BaseApiController
    {
       
    }
}
