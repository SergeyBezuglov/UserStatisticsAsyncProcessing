using Microsoft.AspNetCore.Mvc;
using PIMS.Web.Controllers.Base;

namespace PIMS.Web.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class HomeController: JwtBasedApiController
{
   
    [HttpGet("Index")]
    public IActionResult Index()
    { 
        return Ok(Array.Empty<string>());
    }
   
    
}
