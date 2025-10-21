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
            set { _nombreDepartamento = value ?? string.Empty; } // Asegurando que nunca sea null
        }
        #endregion

        #region constructores
        // constructor vacío necesario para que el "Model Binding" funcione correctamente en ASP.NET MVC
        // este constructor vacío es requerido para permitir que ASP.NET MVC cree un objeto de esta clase 
        // sin pasar parámetros cuando se utilice el model binding.
        public clsDepartamento() { }

        public clsDepartamento(int idDepartamento, string? nombreDepartamento)
        {
            _idDepartamento = idDepartamento;
            _nombreDepartamento = nombreDepartamento ?? string.Empty;  // Asegurando que no sea null
        }
        #endregion
    }
}
