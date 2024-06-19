using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Models.User
{
    /// <summary>
    /// Пользователь создает данные с паролем.
    /// </summary>
    public class UserCreateDataWithPassword: UserCreateBaseData
    {
        /// <summary>
        /// Используем сведения, ранее полученные из форм <see cref="UserCreateDataWithPassword"/> .
        /// </summary>
        /// <param name="firstName">Имя.</param>
        /// <param name="middleName">Отчество.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="email">Электронная почта (email).</param>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        public UserCreateDataWithPassword(string firstName, string middleName, string lastName, string email, string userName,string password):
            base(firstName,middleName, lastName, email, userName)
        { 
            Password = password;
        }
        /// <summary>
        /// Свойство пароль пользователя должно быть передано или установлено при создании экземпляра объекта.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
