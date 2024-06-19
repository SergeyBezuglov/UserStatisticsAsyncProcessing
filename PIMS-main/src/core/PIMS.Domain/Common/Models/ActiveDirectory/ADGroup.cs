using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.Common.Models.ActiveDirectory
{
    /// <summary>
    /// Группа Active Directory.
    /// </summary>
    public class ADGroup
    {
        /// <summary>
        /// Конструктор класса <see cref="ADGroup"/> использует переданные аргументы, чтобы инициализировать свойства объекта класса.
        /// </summary>
        /// <param name="Domain">Домен.</param>
        /// <param name="GroupName">Название группы.</param>
        public ADGroup(string Domain, string GroupName)
        {
            this.GroupName = GroupName;
            this.Domain = Domain;
        }
        /// <summary>
        /// Свойство название группы значения доступны только для чтения и не могут быть изменены извне.
        /// </summary>
        public string GroupName { get; set; }=string.Empty;
        /// <summary>
        /// Свойство домен доступны только для чтения и не могут быть изменены извне.
        /// </summary>
        public string Domain { get; set; } = string.Empty;
        /// <summary>
        /// Возвращает полное имя домена и названия группы.
        /// </summary>
        public string FullName { get { return $"{Domain}\\{GroupName}"; } }
    }
}
