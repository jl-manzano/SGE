namespace Ejercicio4.Models.DAL
{
    public class clsDepartamento
    {
        #region atributos privados
        private int _idDepartamento;
        // se inicializa como un string vacío
        private string? _nombreDepartamento = string.Empty;
        #endregion

        #region getters y setters
        public int IdDepartamento
        {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }

        public string? NombreDepartamento
        {
            get { return _nombreDepartamento; }

            // asegura que nunca sea null
            set { _nombreDepartamento = value ?? string.Empty; }
        }
        #endregion

        #region constructores
        // constructor vacío necesario para el model binding
        public clsDepartamento() { }

        // constructor con parámetros para inicializar los valores
        public clsDepartamento(int idDepartamento, string? nombreDepartamento)
        {
            _idDepartamento = idDepartamento;

            // asegura que no sea null
            _nombreDepartamento = nombreDepartamento ?? string.Empty;
        }
        #endregion
    }
}
