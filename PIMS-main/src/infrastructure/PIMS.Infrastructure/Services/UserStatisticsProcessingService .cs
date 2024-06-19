using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PIMS.Application.Common.Interfaces.Services;
using PIMS.Domain.Common;
using PIMS.Domain.UserStatisticsAsyncProcessingAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Services
{
    public class UserStatisticsProcessingService : BackgroundService
    {
        private readonly ILogger<UserStatisticsProcessingService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly AppSettings _settings;

        public UserStatisticsProcessingService(
            ILogger<UserStatisticsProcessingService> logger,
            IServiceScopeFactory scopeFactory,
            IOptions<AppSettings> settings)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _settings = settings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("UserStatisticsProcessingService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IUserStatisticsRepository>();

                    var pendingRequests = await repository.GetPendingRequestsAsync();
                    var now = DateTime.UtcNow;

                    foreach (var request in pendingRequests)
                    {
                        var elapsed = now - request.CreatedAt;
                        var percentComplete = (int)(elapsed.TotalMilliseconds / _settings.ProcessingTimeInMilliseconds * 100);

                        if (percentComplete >= 100)
                        {
                            // Завершение запроса
                            request.PercentComplete = 100;
                            request.CompletedAt = now;
                            request.Result = GenerateResult(request);
                        }
                        else
                        {
                            request.PercentComplete = percentComplete;
                        }

                        await repository.UpdateRequestAsync(request);
                    }
                }

                await Task.Delay(1000, stoppingToken); // Ждем 1 секунду перед следующей проверкой
            }

            _logger.LogInformation("UserStatisticsProcessingService is stopping.");
        }

        private string GenerateResult(UserStatisticsRequest request)
        {
            // Здесь должна быть логика для генерации результата на основе данных запроса
            var random = new Random();
            var countSignIn = random.Next(1, 100); // Генерация случайного числа для демонстрации

            return $"{{ \"user_id\": \"{request.UserId}\", \"count_sign_in\": \"{countSignIn}\" }}";
        }
    }
}

