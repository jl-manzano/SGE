using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        /// <summary>
        /// Obtiene la lista de todos los departamentos de la base de datos.
        /// </summary>
        /// <returns>Lista de departamentos.</returns>
        public List<Departamento> getDepartamentos()
        {
            // Creamos la conexion
            SqlConnection miConexion = new SqlConnection();

            // Creamos el comando
            SqlCommand miComando = new SqlCommand();

            // Obtenemos la cadena de conexión
            miConexion.ConnectionString = "server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;";

            // Creamos el lector
            SqlDataReader miLector;

            // Creamos la lista de departamentos a devolver
            List<Departamento> departamentos = new List<Departamento>();

            try
            {
                // Abrimos la conexión
                miConexion.Open();

                // Asociamos el comando a la conexión
                miComando.Connection = miConexion;

                // Creamos la consulta Sql
                miComando.CommandText = "SELECT * FROM Departamentos";

                // Ejecutamos la consulta y obtenemos el resultado
                miLector = miComando.ExecuteReader();

                // Leemos el resultado
                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        // Creamos una nueva instancia de Departamento para cada fila
                        Departamento departamento = new Departamento
                        {
                            IdDepartamento = (int)miLector["ID"],
                            NombreDepartamento = (string)miLector["Nombre"]
                        };

                        // Añadimos el departamento al listado
                        departamentos.Add(departamento);
                    }
                }

                // Cerramos el comando
                miLector.Close();
                miConexion.Close();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                throw;
            }

            // Devolvemos los departamentos
            return departamentos;
        }

        /// <summary>
        /// Obtiene un departamento por su ID.
        /// </summary>
        /// <param name="id">ID del departamento a buscar.</param>
        /// <returns>Un departamento con los datos solicitados.</returns>
        public Departamento getDepartamento(int id)
        {
            // Creamos la conexion
            SqlConnection miConexion = new SqlConnection();

            // Creamos el comando
            SqlCommand miComando = new SqlCommand();

            // Obtenemos la cadena de conexión
            miConexion.ConnectionString = "server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;";

            // Creamos el lector
            SqlDataReader miLector;

            // Creamos el departamento
            Departamento departamento = new Departamento();

            try
            {
                // Abrimos la conexión
                miConexion.Open();

                // Asociamos el comando a la conexión
                miComando.Connection = miConexion;

                // Cremos la consulta Sql
                miComando.CommandText = "SELECT * FROM Departamentos WHERE ID = @ID";

                // Asignamos el parámetro de la consulta
                miComando.Parameters.AddWithValue("@ID", id);

                // Ejecutamos y obtenemos el resultado de la consulta
                miLector = miComando.ExecuteReader();

                // Si la consulta devuelve algo
                if (miLector.HasRows)
                {
                    // Recorremos el resultado
                    while (miLector.Read())
                    {
                        // Asignamos los valores
                        departamento.IdDepartamento = (int)miLector["ID"];
                        departamento.NombreDepartamento = (string)miLector["Nombre"];
                    }
                }

                // Cerramos el lector y la conexión
                miLector.Close();
                miConexion.Close();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                throw;
            }

            // Devolvemos el departamento
            return departamento;
        }

        /// <summary>
        /// Añade un nuevo departamento a la base de datos.
        /// </summary>
        /// <param name="departamento">Objeto de tipo Departamento a insertar.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int addDepartamento(Departamento departamento)
        {
            // Creamos la conexión
            SqlConnection miConexion = new SqlConnection();

            // Creamos el comando
            SqlCommand miComando = new SqlCommand();

            // Obtenemos la cadena de conexión
            miConexion.ConnectionString = "server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;";

            try
            {
                // Abrimos la conexión
                miConexion.Open();

                // Asociamos el comando a la conexión
                miComando.Connection = miConexion;

                // Creamos la consulta Sql
                miComando.CommandText = "INSERT INTO DEPARTAMENTOS (Nombre) VALUES (@Nombre)";

                // Asignamos valor al parámetro de la consulta
                miComando.Parameters.AddWithValue("@Nombre", departamento.NombreDepartamento);

                // Ejecutamos la consulta y devolvemos el resultado (filas afectadas)
                return miComando.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Actualiza los datos de un departamento existente.
        /// </summary>
        /// <param name="id">ID del departamento a actualizar.</param>
        /// <param name="departamento">Objeto Departamento con los nuevos datos.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int updateDepartamento(int id, Departamento departamento)
        {
            // Creamos la conexión
            SqlConnection miConexion = new SqlConnection();

            // Creamos el comando
            SqlCommand miComando = new SqlCommand();

            // Obtenemos la cadena de conexión
            miConexion.ConnectionString = "server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;";

            try
            {
                // Abrimos la conexión
                miConexion.Open();

                // Asociamos el comando a la conexión
                miComando.Connection = miConexion;

                // Creamos la consulta Sql
                miComando.CommandText = "UPDATE Departamentos SET Nombre = @Nombre WHERE ID = @ID";

                // Asignamos valor a los parámetros de la consulta
                miComando.Parameters.AddWithValue("@Nombre", departamento.NombreDepartamento);
                miComando.Parameters.AddWithValue("@ID", id);

                // Ejecutamos la consulta y devolvemos el resultado (filas afectadas)
                return miComando.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Elimina un departamento de la base de datos por su ID.
        /// </summary>
        /// <param name="id">ID del departamento a eliminar.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public int deleteDepartamento(int id)
        {
            // Creamos la conexión
            SqlConnection miConexion = new SqlConnection();

            // Creamos el comando
            SqlCommand miComando = new SqlCommand();

            // Obtenemos la cadena de conexión
            miConexion.ConnectionString = "server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;";

            try
            {
                // Abrimos la conexión
                miConexion.Open();

                //Asociamos el comando a la conexión
                miComando.Connection = miConexion;

                // Creamos la consulta Sql
                miComando.CommandText = "DELETE FROM Departamentos WHERE ID = @ID";

                // Asignamos valor al parámetro de la consulta
                miComando.Parameters.AddWithValue("@ID", id);

                // Ejecutamos la consulta y devolvemos el resultado (filas afectadas)
                return miComando.ExecuteNonQuery();
            }
            catch (SqlException SqlEx)
            {
                Console.WriteLine(SqlEx.Message);
                throw;
            }
        }
    }
}
