namespace Ejercicio4.Models.DAL
{
    public class clsDepartamento
    {
        private int _idDepartamento;
        private string _nombreDepartamento;

        public int IdDepartamento
        {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }

        public string NombreDepartamento
        {
            get { return _nombreDepartamento; }
            set { _nombreDepartamento = value; }
        }

        public clsDepartamento(int idDepartamento, string nombreDepartamento)
        {
            _idDepartamento = idDepartamento;
            _nombreDepartamento = nombreDepartamento;
        }
    }
}
