namespace Ejercicio2.Models.DAL
{
    public class clsPersona
    {
        #region atributos privados
        private int _id;
        private string _nombre;
        private string _apellidos;
        private int _edad;
        #endregion

        #region getters y setters
        public int Id
        {
            get { return _id; }
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
        #endregion

        #region constructores
        public clsPersona(int id, string nombre, string apellidos, int edad)
        {
            _id = id;
            _nombre = nombre;
            _apellidos = apellidos;
            _edad = edad;
        }
        #endregion
    }
}