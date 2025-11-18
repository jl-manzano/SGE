namespace Domain.Entities
{
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
        #endregion

        #region Getters y Setters
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Apellidos
        {
            get { return _apellidos; }
            set { _apellidos = value; }
        }

        public DateTime FechaNac
        {
            get { return _fechaNac; }
            set { _fechaNac = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        public string Foto
        {
            get { return _foto; }
            set { _foto = value; }
        }

        #endregion

        #region Constructores
        // Constructor vacío
        public Persona() { }

        // Constructor con parámetros
        public Persona(int id, string nombre, string apellidos, DateTime fechaNac, string direccion, string telefono, string foto)
        {
            _id = id;
            _nombre = nombre;
            _apellidos = apellidos;
            _fechaNac = fechaNac;
            _direccion = direccion;
            _telefono = telefono;
            _foto = foto;
        }
        #endregion
    }
}
