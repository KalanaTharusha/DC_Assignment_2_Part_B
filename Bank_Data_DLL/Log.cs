using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Data_DLL
{
    public class Log
    {
        public int LogId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Action { get; set; }
        public string LogMessage { get; set; }
    }
}
