using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PIMS.Domain.UserStatisticsAsyncProcessingAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация для сущности UserStatisticsRequest.
    /// </summary>
    public class UserStatisticsRequestConfiguration : IEntityTypeConfiguration<UserStatisticsRequest>
    {
        public void Configure(EntityTypeBuilder<UserStatisticsRequest> builder)
        {
            // Задание первичного ключа
            builder.HasKey(e => e.Id);

            // Задание обязательных полей
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.StartDate).IsRequired();
            builder.Property(e => e.EndDate).IsRequired();

            // Задание значения по умолчанию для Result
            builder.Property(e => e.Result).HasMaxLength(500)
                   .HasDefaultValue(string.Empty);
        }
    }
}
