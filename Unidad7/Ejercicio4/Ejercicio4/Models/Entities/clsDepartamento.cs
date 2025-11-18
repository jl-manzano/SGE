namespace Ejercicio4.Models.DAL
{
    public class clsDepartamento
    {
        #region atributos privados
        private int _idDepartamento;
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
            set { _nombreDepartamento = value ?? string.Empty; }
        }
        #endregion

        #region constructores
        public clsDepartamento() { }

        public clsDepartamento(int idDepartamento, string? nombreDepartamento)
        {
            _idDepartamento = idDepartamento;
            _nombreDepartamento = nombreDepartamento ?? string.Empty;
        }
        #endregion
    }
}
