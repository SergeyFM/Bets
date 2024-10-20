using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsService.Models
{
    public sealed class EventRequest
    {
        /// <summary>
        /// Описание события
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Дата и время начала события
        /// </summary>
        public DateTime EventStartTime { get; set; }

        /// <summary>
        /// Дата и время окончания приема ставок на событие
        /// </summary>
        public DateTime BetsEndTime { get; set;}
    }
}
