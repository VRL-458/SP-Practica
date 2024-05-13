using Microsoft.AspNetCore.Mvc;
using LNAT.businesslogic.Managers;
using LNAT.businesslogic.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSP.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ControlerPatients : ControllerBase
    {
        private readonly PacientesManger _managerPatients;

        public ControlerPatients(PacientesManger managerPatients)
        {
            _managerPatients = managerPatients;
        }
        // GET: api/<PATIENTS>
        [HttpGet]
        public IEnumerable<Pacientes> GetCode()
        {
            return _managerPatients.ObtenerPacientes().Values;
        }


        // GET api/<PATIENTS>/5
        [HttpGet("{id}")]
        public Pacientes Get(int id)
        {
            return _managerPatients.obtenerPacienteCI(id);
        }

        // POST api/<PATIENTS>
        [HttpPost]
        public async void Post([FromBody] Pacientes value)
        {
            await _managerPatients.ObtenerPaciente(value.nombre, value.apellido, value.Ci);
        }

        // PUT api/<PATIENTS>/5
        [HttpPut("nombre/{id}")]
        public void PutName(int id, string nombre)
        {
            _managerPatients.updatebyCiNombre(id, nombre);
        }

        [HttpPut("apellido/{id}")]
        public void PutLastName(int id, string apellido)
        {
            _managerPatients.updatebyCiApellido(id, apellido);
        }

        // DELETE api/<PATIENTS>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _managerPatients.remove(id);
        }
    }
}
