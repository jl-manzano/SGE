using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Entities
{
    public class Departamento
    {
        #region atributos privados
        private int _idDepartamento;
        private string _nombreDepartamento;
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
            set { _nombreDepartamento = value; }
        }
        #endregion

        #region constructores
        public Departamento() { }

        public Departamento(int idDepartamento, string nombreDepartamento)
        {
            _idDepartamento = idDepartamento;
            _nombreDepartamento = nombreDepartamento;
        }
        #endregion
    }
}
