using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Mision
    {
        #region atributos privados
        private int _id;
        private string _titulo;
        private string _descripcion;
        private decimal _recompensa;
        #endregion
        #region getters y setters

        public int id
        {
            get { return _id; }
        }

        public string titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public decimal recompensa
        {
            get { return _recompensa; }
            set { _recompensa = value; }
        }

        #endregion
        #region constructores
        public Mision() { }
        public Mision(int id, string titulo, string descripcion, decimal recompensa)
        {
            _id = id;
            _titulo = titulo;
            _descripcion = descripcion;
            _recompensa = recompensa;
        }
        #endregion

    }
}