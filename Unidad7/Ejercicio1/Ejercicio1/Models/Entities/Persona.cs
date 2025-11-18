namespace Ejercicio1.Models.Entities
{
    public class Persona
    {
        #region atributos privados
        private int _id;
        private string _nombre = string.Empty;
        private string _apellidos = string.Empty;
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
        public 
            
            Persona() { }
        public Persona(int id, string nombre, string apellidos, int edad)
        {
            _id = id;
            _nombre = nombre;
            _apellidos = apellidos;
            _edad = edad;
        }
        #endregion
    }
}
