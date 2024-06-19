using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.UserStatisticsAsyncProcessingAggregate
{
    /// <summary>
    /// Представляет запрос на статистику пользователя.
    /// </summary>
    public class UserStatisticsRequest
    {
        /// <summary>
        /// Уникальный идентификатор запроса.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Дата начала периода, за который запрашивается статистика.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода, за который запрашивается статистика.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Дата и время создания запроса.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата и время завершения обработки запроса (если завершен).
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Результат выполнения запроса в виде строки (если доступен).
        /// </summary>
        public string Result { get; set; } = string.Empty;

        /// <summary>
        /// Процент выполнения запроса.
        /// </summary>
        public int PercentComplete { get; set; }
    }
}


