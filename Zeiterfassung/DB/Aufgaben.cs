using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassung.DB
{
    public class Aufgaben
    {
        public int ID { get; set; }
        public string Aufgabe { get; set; }
        public List<int> Teilaufgaben { get; set; }
        public List<string> ComboBox { get; set; }
    }
}
