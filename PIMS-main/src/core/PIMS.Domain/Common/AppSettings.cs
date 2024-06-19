using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common
{
    /// <summary>
    /// Класс для хранения конфигурационных настроек.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Время обработки запроса в миллисекундах.
        /// </summary>
        public int ProcessingTimeInMilliseconds { get; set; }
    }
}
