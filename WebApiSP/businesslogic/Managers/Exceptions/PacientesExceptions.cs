using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNAT.businesslogic.Managers.Exceptions
{
    public class PacientesExceptions : Exception
    {
        public PacientesExceptions() { }
        public PacientesExceptions(string message) : base(message) { }

        public string GetMensajeforLogs(string method)
        {
            return $"{method} Exception: {Message}";
        }

    }
}
