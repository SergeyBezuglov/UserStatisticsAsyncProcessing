using Microsoft.EntityFrameworkCore;
using PIMS.Application.Common.Interfaces.Services;
using PIMS.Domain.UserStatisticsAsyncProcessingAggregate;
using PIMS.Infrastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.Repositories.Common
{
    public class UserStatisticsRepository : IUserStatisticsRepository
    {
        private readonly PIMSDbContext _context;

        public UserStatisticsRepository(PIMSDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateRequestAsync(Guid userId, DateTime startDate, DateTime endDate)
        {
            var request = new UserStatisticsRequest
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                StartDate = startDate,
                EndDate = endDate,
                CreatedAt = DateTime.UtcNow,
                PercentComplete = 0
            };

            _context.UserStatisticsRequests.Add(request);
            await _context.SaveChangesAsync();
            return request.Id;
        }

        public async Task<UserStatisticsRequest> GetRequestAsync(Guid id)
        {
            return await _context.UserStatisticsRequests.FindAsync(id);
        }

        public async Task UpdateRequestAsync(UserStatisticsRequest request)
        {
            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserStatisticsRequest>> GetPendingRequestsAsync()
        {
            return await _context.UserStatisticsRequests
                .Where(r => r.PercentComplete < 100)
                .ToListAsync();
        }
    }
}