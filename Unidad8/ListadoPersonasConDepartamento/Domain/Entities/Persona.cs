using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Representa una persona en el sistema.
    /// </summary>
    public class Persona
    {
        #region Atributos privados
        private int _id;
        private string _nombre;
        private string _apellidos;
        private DateTime _fechaNac;
        private string _direccion;
        private string _telefono;
        private string _foto;
        private int _idDepartamento;
        #endregion

        #region Getters y Setters
        /// <summary>
        /// ID de la persona.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Nombre de la persona.
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        /// <summary>
        /// Apellidos de la persona.
        /// </summary>
        public string Apellidos
        {
            get { return _apellidos; }
            set { _apellidos = value; }
        }

        /// <summary>
        /// Fecha de nacimiento de la persona.
        /// </summary>
        public DateTime FechaNac
        {
            get { return _fechaNac; }
            set { _fechaNac = value; }
        }

        /// <summary>
        /// Dirección de la persona.
        /// </summary>
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        /// <summary>
        /// Teléfono de la persona.
        /// </summary>
        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        /// <summary>
        /// Foto de la persona.
        /// </summary>
        public string Foto
        {
            get { return _foto; }
            set { _foto = value; }
        }

        /// <summary>
        /// ID del departamento al que pertenece la persona.
        /// </summary>
        public int IdDepartamento
        {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor vacío.
        /// </summary>
        public Persona() { }

        /// <summary>
        /// Constructor con parámetros para inicializar los datos de la persona.
        /// </summary>
        /// <param name="id">ID de la persona.</param>
        /// <param name="nombre">Nombre de la persona.</param>
        /// <param name="apellidos">Apellidos de la persona.</param>
        /// <param name="fechaNac">Fecha de nacimiento de la persona.</param>
        /// <param name="direccion">Dirección de la persona.</param>
        /// <param name="telefono">Teléfono de la persona.</param>
        /// <param name="foto">Foto de la persona.</param>
        /// <param name="idDepartamento">ID del departamento de la persona.</param>
        public Persona(int id, string nombre, string apellidos, DateTime fechaNac, string direccion, string telefono, string foto, int idDepartamento)
        {
            _id = id;
            _nombre = nombre;
            _apellidos = apellidos;
            _fechaNac = fechaNac;
            _direccion = direccion;
            _telefono = telefono;
            _foto = foto;
            _idDepartamento = idDepartamento;
        }
        #endregion
    }
}
