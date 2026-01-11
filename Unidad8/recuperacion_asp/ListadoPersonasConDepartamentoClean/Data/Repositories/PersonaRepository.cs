using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Data.Repositories
{
    /// <summary>
    /// Repositorio de acceso a datos para la entidad <see cref="Persona"/>.
    /// Implementa <see cref="IPersonaRepository"/> utilizando ADO.NET para consultar la tabla Personas
    /// y mapear los resultados a objetos de dominio.
    /// </summary>
    public class PersonaRepository : IPersonaRepository
    {
        /// <summary>
        /// Obtiene todas las personas almacenadas en la base de datos.
        /// Ejecuta la consulta: <c>SELECT * FROM Personas</c> y mapea cada fila a un objeto <see cref="Persona"/>.
        /// </summary>
        /// <returns>Lista de <see cref="Persona"/> con la información recuperada.</returns>
        /// <exception cref="SqlException">Se lanza si ocurre un error al abrir la conexión o ejecutar la consulta.</exception>
        public List<Persona> getPersonas()
        {
            SqlConnection miConexion = new SqlConnection();
            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;

            miConexion.ConnectionString = "server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;";

            List<Persona> listaPersonas = new List<Persona>();

            try
            {
                miConexion.Open();
                miComando.Connection = miConexion;
                miComando.CommandText = "SELECT * FROM Personas";

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        Persona persona = new Persona();
                        persona.Id = (int)miLector["ID"];
                        persona.Nombre = (string)miLector["Nombre"];
                        persona.Apellidos = (string)miLector["Apellidos"];
                        persona.FechaNac = (DateTime)miLector["FechaNacimiento"];
                        persona.Direccion = (string)miLector["Direccion"];
                        persona.Telefono = (string)miLector["Telefono"];
                        persona.Foto = (string)miLector["Foto"];

                        // IDDepartamento puede ser NULL en la tabla; en ese caso se asigna 0.
                        if (miLector["IDDepartamento"] != DBNull.Value)
                        {
                            persona.IdDepartamento = (int)miLector["IDDepartamento"];
                        }
                        else
                        {
                            persona.IdDepartamento = 0;
                        }

                        listaPersonas.Add(persona);
                    }
                }

                miLector.Close();
                miConexion.Close();
            }
            catch (SqlException exSql)
            {
                Console.WriteLine(exSql.Message);
                throw;
            }

            return listaPersonas;
        }
    }
}
