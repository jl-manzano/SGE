namespace EjerciciosUnidad7.Models.Entities
{
    public class Persona
    {
        #region atributos privados
        private int _id;
        private string _nombre;
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
        #endregion
        #region constructores
        public Persona(int id, string nombre)
        {
            _id = id;
            _nombre = nombre;
        }
        #endregion
    }
}
