using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class ListadoMisionesVacio : IListadoMisionesRepository
    {
        private List<Mision> _listadoMisiones;

        public ListadoMisionesVacio()
        {
            _listadoMisiones = new List<Mision>();

        }

        public List<Mision> obtenerListadoMisiones()
        {
            return _listadoMisiones;
        }

    }
}
