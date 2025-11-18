using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ListadoConMisionSeleccionadaDto
    {
        #region atributos
        private List<Mision> _listadoMisiones;
        private Mision _misionSeleccionada;
        #endregion

        #region constructores
        public ListadoConMisionSeleccionadaDto(List<Mision> listadoMisiones, Mision misionSeleccionada)
        {
            _listadoMisiones = listadoMisiones;
            _misionSeleccionada = misionSeleccionada;
        }
        public ListadoConMisionSeleccionadaDto() { }
        #endregion

        #region propiedades
        public List<Mision> listadoMisiones
        {
            get { return _listadoMisiones; }
        }
        public Mision misionSeleccionada
        {
            get { return _misionSeleccionada; }
        }
        #endregion

    }
}