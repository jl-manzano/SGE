using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.UseCases.Interfaces;
using Microsoft.Data.SqlClient;

namespace Data.Repositories
{
    public class PersonasRepositoryAzure : IListadoPersonasRepository
    {
        private SqlConnection miConexion = new SqlConnection();
        private List<Persona> listadoPersonas = new List<Persona>();
        private SqlCommand miComando = new SqlCommand();
        private SqlDataReader miLector;
        private Persona oPersona;

        public List<Persona> obtenerListadoPersonas()
        {
            // Establecemos la cadena de conexión
            miConexion.ConnectionString = "server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;";

            try
            {
                // Abrimos la conexión a la base de datos
                miConexion.Open();

                // Creamos el comando para ejecutar la consulta
                miComando.CommandText = "SELECT * FROM personas";
                miComando.Connection = miConexion;

                // Ejecutamos la consulta y obtenemos el lector de datos
                miLector = miComando.ExecuteReader();

                // Si hay filas en el lector
                if (miLector.HasRows)
                {
                    // Leemos las filas
                    while (miLector.Read())
                    {
                        // Creamos una nueva instancia de Persona
                        oPersona = new Persona();

                        // Asignamos los valores a la instancia de Persona
                        oPersona.Id = (int)miLector["ID"];
                        oPersona.Nombre = (string)miLector["Nombre"];
                        oPersona.Apellidos = (string)miLector["Apellidos"];

                        // Si sospechamos que el campo puede ser Null en la base de datos
                        if (miLector["FechaNacimiento"] != DBNull.Value)
                        {
                            oPersona.FechaNac = (DateTime)miLector["FechaNacimiento"];
                        }

                        oPersona.Direccion = (string)miLector["Direccion"];
                        oPersona.Telefono = (string)miLector["Telefono"];
                        oPersona.Foto = (string)miLector["Foto"];

                        // Añadimos la persona a la lista
                        listadoPersonas.Add(oPersona);
                    }
                }

                // Cerramos el lector y la conexión
                miLector.Close();
                miConexion.Close();
            }
            catch (SqlException exSql)
            {
                // En caso de error, lanzamos la excepción
                throw exSql;
            }

            // Devolvemos la lista de personas
            return listadoPersonas;
        }

        public Persona obtenerPersonaId(int id)
        {
            // Establecemos la cadena de conexión
            miConexion.ConnectionString = "server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;";

            try
            {
                // Abrimos la conexión a la base de datos
                miConexion.Open();

                // Creamos el comando para ejecutar la consulta
                miComando.CommandText = "SELECT * FROM personas WHERE ID = @Id";
                miComando.Parameters.AddWithValue("@Id", id);  // Usamos parámetros para evitar inyecciones SQL
                miComando.Connection = miConexion;

                // Ejecutamos la consulta y obtenemos el lector de datos
                miLector = miComando.ExecuteReader();

                // Si hay filas en el lector
                if (miLector.HasRows)
                {
                    // Leemos la primera fila (solo debe haber una persona por id)
                    miLector.Read();

                    // Creamos una nueva instancia de Persona
                    oPersona = new Persona();

                    // Asignamos los valores a la instancia de Persona
                    oPersona.Id = (int)miLector["ID"];
                    oPersona.Nombre = (string)miLector["Nombre"];
                    oPersona.Apellidos = (string)miLector["Apellidos"];

                    // Si sospechamos que el campo puede ser Null en la base de datos
                    if (miLector["FechaNacimiento"] != DBNull.Value)
                    {
                        oPersona.FechaNac = (DateTime)miLector["FechaNacimiento"];
                    }

                    oPersona.Direccion = (string)miLector["Direccion"];
                    oPersona.Telefono = (string)miLector["Telefono"];
                    oPersona.Foto = (string)miLector["Foto"];
                }

                // Cerramos el lector y la conexión
                miLector.Close();
                miConexion.Close();
            }
            catch (SqlException exSql)
            {
                // En caso de error, lanzamos la excepción
                throw exSql;
            }

            // Devolvemos la persona encontrada
            return oPersona;
        }

        public void insertarPersona(Persona persona)
        {
            using (SqlConnection miConexion = new SqlConnection("server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;"))
            {
                try
                {
                    miConexion.Open();
                    string query = "INSERT INTO personas (Nombre, Apellidos, FechaNacimiento, Direccion, Telefono, Foto) VALUES (@Nombre, @Apellidos, @FechaNacimiento, @Direccion, @Telefono, @Foto)";
                    using (SqlCommand miComando = new SqlCommand(query, miConexion))
                    {
                        miComando.Parameters.AddWithValue("@Nombre", persona.Nombre);
                        miComando.Parameters.AddWithValue("@Apellidos", persona.Apellidos);
                        miComando.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNac);
                        miComando.Parameters.AddWithValue("@Direccion", persona.Direccion);
                        miComando.Parameters.AddWithValue("@Telefono", persona.Telefono);
                        miComando.Parameters.AddWithValue("@Foto", persona.Foto);

                        miComando.ExecuteNonQuery();  // Ejecuta la inserción
                    }
                }
                catch (SqlException exSql)
                {
                    throw exSql;
                }
            }
        }


        public void actualizarPersona(Persona persona)
        {
            using (SqlConnection miConexion = new SqlConnection("server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;"))
            {
                try
                {
                    miConexion.Open();
                    string query = "UPDATE personas SET Nombre = @Nombre, Apellidos = @Apellidos, FechaNacimiento = @FechaNacimiento, Direccion = @Direccion, Telefono = @Telefono, Foto = @Foto WHERE ID = @Id";
                    using (SqlCommand miComando = new SqlCommand(query, miConexion))
                    {
                        miComando.Parameters.AddWithValue("@Id", persona.Id);
                        miComando.Parameters.AddWithValue("@Nombre", persona.Nombre);
                        miComando.Parameters.AddWithValue("@Apellidos", persona.Apellidos);
                        miComando.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNac);
                        miComando.Parameters.AddWithValue("@Direccion", persona.Direccion);
                        miComando.Parameters.AddWithValue("@Telefono", persona.Telefono);
                        miComando.Parameters.AddWithValue("@Foto", persona.Foto);

                        miComando.ExecuteNonQuery();  // Ejecuta la actualización
                    }
                }
                catch (SqlException exSql)
                {
                    // En caso de error, lanzamos la excepción
                    throw exSql;
                }
            }
        }

        public void eliminarPersona(int id)
        {
            using (SqlConnection miConexion = new SqlConnection("server=josemnzano.database.windows.net;database=PersonasDB;uid=jlmanzano;pwd=abc12345_;trustServerCertificate=true;"))
            {
                try
                {
                    miConexion.Open();
                    string query = "DELETE FROM personas WHERE ID = @Id";
                    using (SqlCommand miComando = new SqlCommand(query, miConexion))
                    {
                        miComando.Parameters.AddWithValue("@Id", id);

                        miComando.ExecuteNonQuery();  // Ejecuta la eliminación
                    }
                }
                catch (SqlException exSql)
                {
                    // En caso de error, lanzamos la excepción
                    throw exSql;
                }
            }
        }

    }
}