using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    /// <summary>
    /// Repositorio de acceso a datos para la entidad <see cref="Departamento"/>.
    /// Implementa <see cref="IDepartamentoRepository"/> realizando consultas a la tabla Departamentos
    /// mediante ADO.NET (SqlConnection, SqlCommand y SqlDataReader).
    /// </summary>
    public class DepartamentoRepository : IDepartamentoRepository
    {
        /// <summary>
        /// Obtiene la lista completa de departamentos desde la base de datos.
        /// Ejecuta la consulta: <c>SELECT * FROM Departamentos</c> y mapea cada registro a un objeto
        /// <see cref="Departamento"/>.
        /// </summary>
        /// <returns>Lista de <see cref="Departamento"/> con los datos recuperados.</returns>
        /// <exception cref="SqlException">Se lanza si ocurre un error al abrir la conexión o ejecutar la consulta.</exception>
        public List<Departamento> getDepartamentos()
        {
            SqlConnection miConexion = new SqlConnection();
            SqlCommand miComando = new SqlCommand();

            miConexion.ConnectionString = "server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;";

            SqlDataReader miLector;
            List<Departamento> departamentos = new List<Departamento>();

            try
            {
                miConexion.Open();
                miComando.Connection = miConexion;
                miComando.CommandText = "SELECT * FROM Departamentos";

                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        Departamento departamento = new Departamento
                        {
                            IdDepartamento = (int)miLector["ID"],
                            NombreDepartamento = (string)miLector["Nombre"]
                        };

                        departamentos.Add(departamento);
                    }
                }

                miLector.Close();
                miConexion.Close();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                throw;
            }

            return departamentos;
        }
    }
}
