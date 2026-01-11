using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Representa un departamento en el sistema.
    /// </summary>
    public class Departamento
    {
        #region atributos privados
        private int _idDepartamento;
        private string _nombreDepartamento;
        #endregion

        #region getters y setters
        /// <summary>
        /// ID del departamento.
        /// </summary>
        public int IdDepartamento
        {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }

        /// <summary>
        /// Nombre del departamento.
        /// </summary>
        public string? NombreDepartamento
        {
            get { return _nombreDepartamento; }
            set { _nombreDepartamento = value; }
        }
        #endregion

        #region constructores
        /// <summary>
        /// Constructor vacío.
        /// </summary>
        public Departamento() { }

        /// <summary>
        /// Constructor con parámetros para inicializar un departamento.
        /// </summary>
        /// <param name="idDepartamento">ID del departamento.</param>
        /// <param name="nombreDepartamento">Nombre del departamento.</param>
        public Departamento(int idDepartamento, string nombreDepartamento)
        {
            _idDepartamento = idDepartamento;
            _nombreDepartamento = nombreDepartamento;
        }
        #endregion
    }
}
