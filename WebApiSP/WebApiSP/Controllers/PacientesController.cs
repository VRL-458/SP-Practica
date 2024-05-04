using Microsoft.AspNetCore.Mvc;
using LNAT.businesslogic.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSP.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ControlerPatients : ControllerBase
    {
        private readonly PacientesManger _managerPatients;

        public ControlerPatients(ManagerPatients managerPatients)
        {
            _managerPatients = managerPatients;

            //funcion leer archivo
            //recorrer cada paciene y agregue
        }
        //ManagerPatients pacientes = new ManagerPatients(); revisar
        // GET: api/<PATIENTS>
        [HttpGet]
        public IEnumerable<Patients> Get()
        {
            return _managerPatients.ObtenerPacientes().Values;
        }

        // GET api/<PATIENTS>/5
        [HttpGet("{id}")]
        public Patients Get(int id)
        {
            return _managerPatients.obtenerPacienteCI(id);
        }

        // POST api/<PATIENTS>
        [HttpPost]
        public void Post([FromBody] Patients value)
        {
            _managerPatients.addPaciente(value);
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
