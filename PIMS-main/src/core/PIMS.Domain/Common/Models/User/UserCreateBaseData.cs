
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Models.User
{
    /// <summary>
    /// Пользователь создает базу данных.
    /// </summary>
    public class UserCreateBaseData
    {
        /// <summary>
        /// Используем сведения, ранее полученные из форм <see cref="UserCreateBaseData"/> .
        /// </summary>
        /// <param name="firstName">Имя.</param>
        /// <param name="middleName">Отчество.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="email">Электронная почта.</param>
        /// <param name="userName">Имя пользователя.</param>
        public UserCreateBaseData(string firstName,string middleName, string lastName,string email,string userName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            UserName = userName;               
        }
        /// <summary>
        /// Свойство имя должно быть передано или установлено при создании экземпляра объекта.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// Свойство отчество должно быть передано или установлено при создании экземпляра объекта.
        /// </summary>
        public string MiddleName { get; set; } = string.Empty;
        /// <summary>
        /// Свойство фамилия должно быть передано или установлено при создании экземпляра объекта.
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// Свойство электронная почта должно быть передано или установлено при создании экземпляра объекта.
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Свойство имя пользователя должно быть передано или установлено при создании экземпляра объекта.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }
}
