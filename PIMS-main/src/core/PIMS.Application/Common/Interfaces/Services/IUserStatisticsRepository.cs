using PIMS.Domain.UserStatisticsAsyncProcessingAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Common.Interfaces.Services
{
    public interface IUserStatisticsRepository
    {
        Task<Guid> CreateRequestAsync(Guid userId, DateTime startDate, DateTime endDate);
        Task<UserStatisticsRequest> GetRequestAsync(Guid id);
        Task UpdateRequestAsync(UserStatisticsRequest request);
        Task<List<UserStatisticsRequest>> GetPendingRequestsAsync();
    }
}
