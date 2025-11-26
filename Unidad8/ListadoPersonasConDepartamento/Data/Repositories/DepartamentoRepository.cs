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
    public class DepartamentoRepository: IDepartamentoRepository
    {
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

            // Creamos el departamento que vamos a ir añadiendo
            Departamento departamento = new Departamento();

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
                        // Obtenemos el departamento
                        departamento.IdDepartamento = (int)miLector["ID"];
                        departamento.NombreDepartamento = (string)miLector["Nombre"];

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

    }

}