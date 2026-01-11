using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataResources
{
    /// <summary>
    /// Gestiona la configuración de conexión a SQL Server y proporciona métodos para abrir y cerrar conexiones.
    /// Permite inicializar credenciales/servidor por defecto o mediante parámetros.
    /// </summary>
    public class clsMyConnection
    {
        /// <summary>
        /// Nombre o dirección del servidor SQL.
        /// </summary>
        public String server { get; set; }

        /// <summary>
        /// Nombre de la base de datos a la que se conectará.
        /// </summary>
        public String dataBase { get; set; }

        /// <summary>
        /// Usuario utilizado para autenticación en el servidor SQL.
        /// </summary>
        public String user { get; set; }

        /// <summary>
        /// Contraseña del usuario utilizado para autenticación en el servidor SQL.
        /// </summary>
        public String pass { get; set; }

        /// <summary>
        /// Constructor por defecto: carga valores predefinidos de servidor, base de datos, usuario y contraseña.
        /// </summary>
        public clsMyConnection()
        {
            this.server = "josemnzano.database.windows.net";
            this.dataBase = "PersonasDB";
            this.user = "jlmanzano";
            this.pass = "abc12345_";
        }

        /// <summary>
        /// Constructor parametrizado: permite establecer servidor, base de datos, usuario y contraseña manualmente.
        /// </summary>
        /// <param name="server">Servidor SQL.</param>
        /// <param name="database">Base de datos.</param>
        /// <param name="user">Usuario.</param>
        /// <param name="pass">Contraseña.</param>
        public clsMyConnection(String server, String database, String user, String pass)
        {
            this.server = server;
            this.dataBase = database;
            this.user = user;
            this.pass = pass;
        }

        /// <summary>
        /// Crea y abre una conexión a SQL Server usando los datos configurados en la instancia.
        /// </summary>
        /// <returns>Una instancia de <see cref="SqlConnection"/> abierta.</returns>
        /// <exception cref="SqlException">Se lanza si ocurre un error al abrir la conexión.</exception>
        public SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = $"server={server};database={dataBase};uid={user};pwd={pass};trustServerCertificate=true;";
                connection.Open();
            }
            catch (SqlException)
            {
                throw;
            }
            return connection;
        }

        /// <summary>
        /// Cierra una conexión abierta recibida por referencia.
        /// </summary>
        /// <param name="connection">Conexión a cerrar.</param>
        /// <exception cref="SqlException">Se lanza si ocurre un error relacionado con SQL al cerrar.</exception>
        /// <exception cref="InvalidOperationException">Se lanza si la conexión no está en un estado válido para cerrarse.</exception>
        /// <exception cref="Exception">Se lanza ante cualquier otro error inesperado.</exception>
        public void closeConnection(ref SqlConnection connection)
        {
            try
            {
                connection.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
