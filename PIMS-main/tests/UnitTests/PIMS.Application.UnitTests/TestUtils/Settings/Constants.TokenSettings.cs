using PIMS.Domain.Common.Authentication.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.UnitTests.TestUtils.Settings
{
    /// <summary>
    /// Константы.
    /// </summary>
    public static partial class Constants
    {

        public static class JwtSettingsDefault
        {
            /// <summary>
            /// The jwt settings.
            /// </summary>
            public static readonly JwtSettings JwtSettings =new JwtSettings("D23083AA-4C1F-4772-AA6B-B926A69000DD", 100,"PIMS","PIMS Audience");
        }
    }
}
