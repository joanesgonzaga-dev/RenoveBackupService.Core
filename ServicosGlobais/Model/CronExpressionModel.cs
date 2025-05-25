using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicosGlobais.Model
{
    /// <summary>
    /// Classe que representa os campos estruturais de uma expressão CRON do Quartz
    /// </summary>
    public class CronExpressionModel
    {
        public string Seconds { get; set; } = "0";
        public string Minutes { get; set; }
        public string Hours { get; set; }
        public string DayOfMonth { get; set; }
        public string Month { get; set; }
        public string DayOfWeek { get; set; }
        public string RecurrenceTime { get; set; }
        public string RecurrenceType { get; set; }
    }
}
