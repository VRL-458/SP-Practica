
using LNAT.businesslogic.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace LNAT.businesslogic.Managers
{
    public class PacientesManger
    {
        private readonly string _filePath;
        private readonly string _fileLog;
        private readonly IConfiguration _configuration;

        Dictionary<int, Pacientes> pacientes = new Dictionary<int, Pacientes>();
        public PacientesManger(IConfiguration configuration)
        {

            _configuration = configuration;

            try
            {
                _filePath = _configuration.GetSection("Paths").GetSection("txt").Value;
            }
            catch (Exception ex)
            {
                /*
                BackingServiceException bsEx = new BackingServiceException(ex.Message);
                Log.Error(bsEx.GetMessageLogs("Configurin file Path"));
                */

            }

            LoadPatientsFromFile();

        }
        public void addPorParametros(string nombre, string apellido, int ci)
        {
            pacientes.Add(ci, new Pacientes()
            {
                nombre = nombre,
                apellido = apellido,
                Ci = ci,

            }
            );
            EscribirPacientesEnArchivo();
        }
        public void addPaciente(Pacientes paciente)
        {
            paciente.GetRandomBloodGroup();
            pacientes.Add(paciente.Ci, paciente);
            EscribirPacientesEnArchivo();
        }
        public void remove(int ci)
        {
            try
            {
                pacientes.Remove(ci);
                EscribirPacientesEnArchivo();
            }
            catch (Exception ex)
            {
                /*
                Log.Error($"Error ocurrido {ex}");
                */
            }
        }

        public Pacientes obtenerPacienteCI(int ci)
        {
            try
            {
                return pacientes[ci];
            }
            catch (Exception ex)
            {
                /*
                Log.Error($"Error ocurrido {ex}");

                */
                return null;
            }
           
        }
        public string updatebyCiApellido(int ci, string apellido)
        {
            try
            {
                pacientes[ci].apellido = apellido;
                EscribirPacientesEnArchivo();
                return "Datos actualizados";
            }
            catch (Exception ex)
            {
                return "Patient not found";
            }
        }
        public string updatebyCiNombre(int ci, string nombre)
        {
            if (pacientes[ci] != null)
            {
                pacientes[ci].nombre = nombre;
                EscribirPacientesEnArchivo();


                return "Datos actualizados";

            }
            else
                return "Patient not found";
        }
        public Dictionary<int, Pacientes> ObtenerPacientes()
        {
            return pacientes;
        }
        //funcion add patientes a una lista --
        //eliminar por Ci--
        //get by CI--
        //update by CI--
        // get paticientes--
        //leer archivo txt
        //borrar archivo

        public void LoadPatientsFromFile()
        {

            if (File.Exists(_filePath))
            {
                var lines = File.ReadAllLines(_filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    var patient = new Pacientes()
                    {
                        nombre = parts[0],
                        apellido = parts[1],
                        Ci = int.Parse(parts[2]),
                        tipoSangre = parts[3],
                    };
                    pacientes.Add(int.Parse(parts[2]), patient);
                }
            }
        }

        public void EscribirPacientesEnArchivo()
        {
            using (StreamWriter writer = new StreamWriter(_filePath, false)) // El segundo parámetro 'false' significa que sobreescribirá el archivo si existe
            {
                foreach (Pacientes paciente in pacientes.Values)
                {
                    string linea = $"{paciente.nombre},{paciente.apellido},{paciente.Ci},{paciente.tipoSangre}";
                    writer.WriteLine(linea);
                }
            }
        }

    }
}
