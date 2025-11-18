using Domain.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases
{
    public class ListadoMisionesUC : IListadoMisionesUC
    {
        private readonly IListadoMisionesRepository _listadoMisionesRepository;
        public ListadoMisionesUC(IListadoMisionesRepository listadoMisionesRepository)
        {
            _listadoMisionesRepository = listadoMisionesRepository;
        }

        public List<Mision> obtenerListadoMisiones()
        {
            return _listadoMisionesRepository.obtenerListadoMisiones();
        }

    }
}
