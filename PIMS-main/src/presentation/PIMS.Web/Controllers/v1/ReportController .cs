using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PIMS.Application.Common.Interfaces.Services;
using PIMS.Domain.Common;

namespace PIMS.Web.Controllers.v1
{
    [ApiController]
    [Route("report")]
    public class ReportController : ControllerBase
    {
        private readonly IUserStatisticsRepository _repository;
        private readonly AppSettings _settings;

        public ReportController(IUserStatisticsRepository repository, IOptions<AppSettings> settings)
        {
            _repository = repository;
            _settings = settings.Value;
        }

        [HttpPost("user_statistics")]
        public async Task<IActionResult> PostUserStatistics([FromBody] UserStatisticsRequestDto dto)
        {
            var id = await _repository.CreateRequestAsync(dto.UserId, dto.StartDate, dto.EndDate);
            return Ok(new { query = id });
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetReportInfo([FromQuery] Guid query)
        {
            var request = await _repository.GetRequestAsync(query);
            if (request == null)
            {
                return NotFound();
            }

            var elapsed = DateTime.UtcNow - request.CreatedAt;
            var percentComplete = (int)(elapsed.TotalMilliseconds / _settings.ProcessingTimeInMilliseconds * 100);
            if (percentComplete > 100) percentComplete = 100;

            return Ok(new
            {
                query = request.Id,
                percent = percentComplete,
                result = request.PercentComplete == 100 ? request.Result : null
            });
        }
    }

    public class UserStatisticsRequestDto
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

