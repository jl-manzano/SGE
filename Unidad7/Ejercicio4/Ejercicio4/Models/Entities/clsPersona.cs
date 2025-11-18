namespace Ejercicio4.Models.DAL
{
    public class clsPersona
    {
        #region atributos privados
        private int _id;
        private string _nombre = string.Empty;
        private string _apellidos = string.Empty;
        private int _edad;
        private int _idDepartamento;
        #endregion

        #region getters y setters
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

        public int Edad
        {
            get { return _edad; }
            set { _edad = value; }
        }

        public int IdDepartamento
        {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }
        #endregion

        #region constructores
        public clsPersona()
        {
        }

        public clsPersona(int id, string nombre, string apellidos, int edad, int idDepartamento)
        {
            _id = id;
            _nombre = nombre;
            _apellidos = apellidos;
            _edad = edad;
            _idDepartamento = idDepartamento;
        }
        #endregion
    }
}
