using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Authentication.Configuration
{
    /// <summary>
    /// Настройки jwt.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Название раздела.
        /// </summary>
        public const string SectionName = "JwtSettings";
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="JwtSettings"/> .
        /// </summary>
        public JwtSettings()
        {

        }
        /// <summary>
        /// Конструктор для объекта JwtSettings>, устанавливает значения параметров для соответствующих свойств объекта.
        /// </summary>
        /// <param name="secret">Секрет.</param>
        /// <param name="expiryDays">Истечение срока действия.</param>
        /// <param name="issuer">Эмитент.</param>
        /// <param name="audience">Аудитория.</param>
        public JwtSettings(string secret,int expiryDays,string issuer,string audience)
        {
            Secret= secret;
            ExpiryDays = expiryDays;
            Issuer= issuer;
            Audience = audience;
        }
        /// <summary>
        /// Свойство секрет можно установить значение только во время инициализации объекта.
        /// </summary>
        public string Secret { get; init; } = string.Empty;
        /// <summary>
        /// Свойство дни истечения срока действия можно установить значение только во время инициализации объекта.
        /// </summary>
        public int ExpiryDays { get; init; } = 0;
        /// <summary>
        /// Свойство приложение, из которого отправляется токен, можно установить значение только во время инициализации объекта.
        /// </summary>
        public string Issuer { get; init; } = string.Empty;
        /// <summary>
        /// Свойство потребитель токена можно установить значение только во время инициализации объекта.
        /// </summary>
        public string Audience { get; init; } = string.Empty; 

    }
}
