using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Models
{
    public sealed class UpdateMessengerRequest
    {
        /// <summary>
        /// Идентификатор мессенджера
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование мессенджера
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Кто последний раз изменял
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
