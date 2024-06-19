using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Authentication.Configuration
{
    /// <summary>
    /// Перечисления.
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// Поставщики аутентификации.
        /// </summary>
        public enum AuthenticationProviders
        {
            NotSet,
            JWT,
            ActiveDirectory
        }
    }
}
