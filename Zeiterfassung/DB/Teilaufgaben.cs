using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassung.DB
{
    public class Teilaufgaben
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Dictionary<DateTime, DateTime> Zeitspanne { get; set; }
    }
}
