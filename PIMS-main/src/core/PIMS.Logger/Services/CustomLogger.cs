using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Logger.Services
{
    /// <summary>
    /// Пользовательские расширения журнала.
    /// </summary>
    public static class CustomLoggerExtensions
    {
        public const string UserInfoCustomColumnName = "UserInfo";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">Регистратор.</param>
        /// <param name="level">Уровень.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="workingUserInfo">Информация о рабочем пользователе.</param>
        /// <param name="args">Аргументы.</param>
        public static void LogWithUserInfo(this ILogger logger, LogLevel level, string message, string workingUserInfo, params object?[] args)
        {
            
            using (logger.BeginScope(
            new Dictionary<string, object> { { CustomLoggerExtensions.UserInfoCustomColumnName, workingUserInfo } }))
            {
                BaseLog(logger, level, message, args);
            }           
        }
        /// <summary>
        /// Основывает журнал.
        /// </summary>
        /// <param name="logger">Регистратор.</param>
        /// <param name="level">Уровень.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="args">Аргументы.</param>
        private static void BaseLog( ILogger logger, LogLevel level, string message, params object?[] args)
        {
            logger.Log(level, message, args);
        }

    }
}
