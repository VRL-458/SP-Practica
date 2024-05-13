using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

//revisar esto, cuidado 

namespace LNAT.businesslogic.Models
{
    public class Pacientes
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int Ci { get; set; }
        public string tipoSangre { get; set; }
        public string Code { get; set; }
        public Pacientes()
        {

        }
        public void GetRandomBloodGroup()
        {
            var bloodGroups = new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            var random = new Random();
            tipoSangre = bloodGroups[random.Next(bloodGroups.Length)];
        }

    }
}
