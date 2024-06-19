using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Models.ActiveDirectory
{
    /// <summary>
    /// Пользователь Active Directory.
    /// </summary>
    public class ActiveDirectoryUser
    {
        /// <summary>
        /// К свойству группы присваивается новый экземпляр класса List<ADGroup>, созданный при помощи конструктора по умолчанию.
        /// </summary>
        private IList<ADGroup> _groups { get; } = new List<ADGroup>();
        /// <summary>
        /// Возвращает список групп пользователей AD в виде неизменяемого списка только для чтения.
        /// </summary>
        public IReadOnlyList<ADGroup> Groups { get { return _groups.AsReadOnly(); } }
        /// <summary>
        /// Свойство имя пользователя значения доступны только для чтения и не могут быть изменены извне. 
        /// </summary>
        public string UserName { get; private set; } =string.Empty;
        /// <summary>
        /// Свойство имя значения доступны только для чтения и не могут быть изменены извне.
        /// </summary>
        public string FirstName { get; private set; } = string.Empty;
        /// <summary>
        /// Свойство отчество пользователя значения доступны только для чтения и не могут быть изменены извне.
        /// </summary>
        public string MiddleName { get; private set; } = string.Empty;
        /// <summary>
        /// Свойство фамилия значения доступны только для чтения и не могут быть изменены извне.
        /// </summary>
        public string LastName { get; private set; } = string.Empty;
        /// <summary>
        /// Свойство адрес электронной почты пользователя значения доступны только для чтения и не могут быть изменены извне.
        /// </summary>
        public string? Email { get; private set; } = null;
        /// <summary>
        /// Свойство номер телефона значения доступны только для чтения и не могут быть изменены извне.
        /// </summary>
        public string? PhoneNumber { get; private set; } = null;
        /// <summary>
        /// Свойство название организации доступны только для чтения и не могут быть изменены извне.
        /// </summary>
        public string? OrganizationName { get; private set; } = null;
        /// <summary>
        /// конструктор класса <see cref="ActiveDirectoryUser"/> использует переданные аргументы, чтобы инициализировать свойства объекта класса.
        /// </summary>
        /// <param name="UserName">Имя пользователя.</param>
        /// <param name="FirstName">Имя.</param>
        /// <param name="MiddleName">Отчество.</param>
        /// <param name="LastName">Фамилия.</param>
        /// <param name="Email">Адрес электронной почты пользователя.</param>
        /// <param name="PhoneNumber">Номер телефона.</param>
        /// <param name="OrganizationName">Название организации.</param>
        public ActiveDirectoryUser(string UserName, string FirstName, string MiddleName, string LastName, string? Email = null, string? PhoneNumber = null, string? OrganizationName = null)
        {
            this.UserName = UserName;
            this.FirstName = FirstName;
            this.MiddleName = MiddleName;
            this.LastName = LastName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.OrganizationName = OrganizationName;
        }
        
    }
}
