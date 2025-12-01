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
    public class PersonaRepository: IPersonaRepository
    {
        public List<Persona> getPersonas()
        {
            // Creamos la conexión
            SqlConnection miConexion = new SqlConnection();

            // Creamos el comando
            SqlCommand miComando = new SqlCommand();

            // Creamos el lector
            SqlDataReader miLector;

            // Obtenemos la cadena de conexión
            miConexion.ConnectionString = "server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;";

            // Creamos una lista de personas vacía
            List<Persona> listaPersonas = new List<Persona>();

            try
            {
                // Abrimos la conexión
                miConexion.Open();

                // Asignamos el comando a la conexión
                miComando.Connection = miConexion;

                // Creamos la consulta Sql
                miComando.CommandText = "SELECT * FROM Personas";

                // Ejecutamos la consulta
                miLector = miComando.ExecuteReader();

                // Si la consulta devuelve filas
                if (miLector.HasRows)
                {
                    // Recorremos las filas
                    while (miLector.Read())
                    {
                        // Creamos una persona
                        Persona persona = new Persona();
                        // Asignamos los valores a la persona
                        persona.Id = (int)miLector["ID"];
                        persona.Nombre = (string)miLector["Nombre"];
                        persona.Apellidos = (string)miLector["Apellidos"];
                        persona.FechaNac = (DateTime)miLector["FechaNacimiento"];
                        persona.Direccion = (string)miLector["Direccion"];
                        persona.Telefono = (string)miLector["Telefono"];
                        persona.Foto = (string)miLector["Foto"];

                        // Verificamos si el valor de IDDepartamento es DBNull antes de asignarlo
                        if (miLector["IDDepartamento"] != DBNull.Value)
                        {
                            persona.IdDepartamento = (int)miLector["IDDepartamento"];
                        }
                        else
                        {
                            persona.IdDepartamento = 0; // O asigna un valor predeterminado si es NULL
                        }

                        // Añadimos la persona a la lista
                        listaPersonas.Add(persona);
                    }
                }

                // Cerramos el lector y la conexión
                miLector.Close();
                miConexion.Close();

            } // Capturamos y lanzamos la excepcion
            catch (SqlException exSql)
            {
                Console.WriteLine(exSql.Message);
                throw;
            }

            // Devolvemos la lista de personas
            return listaPersonas;
        }

        public Persona getPersona(int id)
        {
            // Creamos la conexión
            SqlConnection miConexion = new SqlConnection();

            // Creamos el comando
            SqlCommand miComando = new SqlCommand();

            // Creamos el lector
            SqlDataReader miLector;

            // Obtenemos la cadena de conexión
            miConexion.ConnectionString = "server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;";

            // Creamos un objeto persona
            Persona persona = new Persona();

            try
            {
                // Abrimos la conexión
                miConexion.Open();

                // Asignamos el comando a la conexión
                miComando.Connection = miConexion;

                // Creamos la consulta Sql
                miComando.CommandText = "SELECT * FROM Personas WHERE ID = @id";

                // Asignamos el parámetro @id al valor que nos llega en el método
                miComando.Parameters.AddWithValue("@id", id);

                // Ejecutamos la consulta
                miLector = miComando.ExecuteReader();

                // Si la consulta devuelve filas
                if (miLector.HasRows)
                {
                    // Recorremos las filas
                    while (miLector.Read())
                    {
                        // Asignamos los valores a la persona
                        persona.Id = (int)miLector["ID"];
                        persona.Nombre = (string)miLector["Nombre"];
                        persona.Apellidos = (string)miLector["Apellidos"];
                        persona.FechaNac = (DateTime)miLector["FechaNacimiento"];
                        persona.Direccion = (string)miLector["Direccion"];
                        persona.Telefono = (string)miLector["Telefono"];
                        persona.Foto = (string)miLector["Foto"];
                        persona.IdDepartamento = (int)miLector["IDDepartamento"];
                    }
                }

                // Cerramos el lector y la conexión
                miLector.Close();
                miConexion.Close();

            }
            catch (SqlException exSql)
            {
                Console.WriteLine(exSql.Message);
                throw;
            }

            // Devolvemos la persona
            return persona;
        }

        public int addPersona(Persona persona)
        {
            // Creamos una conexion sql
            SqlConnection miConexion = new SqlConnection();

            // Creamos un comando para nuestra conexion
            SqlCommand miComando = new SqlCommand();

            // Inicializamos la conexion
            miConexion.ConnectionString = "server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;";

            try
            {
                // Abrimos la conexion
                miConexion.Open();

                // Asignamos el comando a la conexion
                miComando.Connection = miConexion;


                // Creamos la consulta Sql
                miComando.CommandText = "INSERT INTO Personas (Nombre, Apellidos, Telefono, Direccion, Foto, FechaNacimiento, IDDepartamento) VALUES (@Nombre, @Apellidos, @Telefono, @Direccion, @Foto, @FechaNacimiento, @IDDepartamento)";

                // Asignamos los valores de la persona a la consulta
                miComando.Parameters.AddWithValue("@Nombre", persona.Nombre);
                miComando.Parameters.AddWithValue("@Apellidos", persona.Apellidos);
                miComando.Parameters.AddWithValue("@Telefono", persona.Telefono);
                miComando.Parameters.AddWithValue("@Direccion", persona.Direccion);
                miComando.Parameters.AddWithValue("@Foto", persona.Foto);
                miComando.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNac);
                miComando.Parameters.AddWithValue("@IDDepartamento", persona.IdDepartamento);

                // Ejecutamos la consulta y devolvemos su resultado
                return miComando.ExecuteNonQuery();

            }
            catch (SqlException SqlEx)
            {
                Console.WriteLine(SqlEx.ToString());
                throw;
            }
        }

        public int updatePersona(int id, Persona persona)
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

                // Asignamos el comando a la conexión
                miComando.Connection = miConexion;

                // Creamos la consulta Sql
                miComando.CommandText = "UPDATE Personas SET Nombre = @nombre, Apellidos = @apellidos, FechaNacimiento = @fechaNac, Direccion = @direccion, " +
                                        "Telefono = @telefono, Foto = @foto, IDDepartamento = @idDepartamento WHERE ID = @id";

                // Asignamos los valores de los parámetros
                miComando.Parameters.AddWithValue("@nombre", persona.Nombre);
                miComando.Parameters.AddWithValue("@apellidos", persona.Apellidos);
                miComando.Parameters.AddWithValue("@fechaNac", persona.FechaNac);
                miComando.Parameters.AddWithValue("@direccion", persona.Direccion);
                miComando.Parameters.AddWithValue("@telefono", persona.Telefono);
                miComando.Parameters.AddWithValue("@foto", persona.Foto);
                miComando.Parameters.AddWithValue("@idDepartamento", persona.IdDepartamento);
                miComando.Parameters.AddWithValue("@id", id); // Añadido el parámetro @id

                // Ejecutamos la consulta y obtenemos el número de filas afectadas
                return miComando.ExecuteNonQuery();

            }
            catch (SqlException exSql)
            {
                Console.WriteLine(exSql.Message);
                throw;
            }
        }

        public int deletePersona(int id)
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

                // Asignamos el comando a la conexión
                miComando.Connection = miConexion;

                // Creamos la consulta Sql
                miComando.CommandText = "DELETE FROM Personas WHERE ID = @id";

                // Asignamos el parámetro @id
                miComando.Parameters.AddWithValue("@id", id);

                // Ejecutamos la consulta y obtenemos el número de filas afectadas
                return miComando.ExecuteNonQuery();

            }
            catch (SqlException exSql)
            {
                Console.WriteLine(exSql.Message);
                throw;
            }
        }


    }

}
