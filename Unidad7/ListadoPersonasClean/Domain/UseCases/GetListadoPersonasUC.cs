using Domain.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases
{
    public class GetListadoPersonasUC : IListadoPersonasUseCases
    {
        private readonly IListadoPersonasRepository _listadoPersonasRepository;
        public GetListadoPersonasUC(IListadoPersonasRepository listadoPersonasRepository)
        {
            _listadoPersonasRepository = listadoPersonasRepository;
        }
        public List<Entities.Persona> obtenerListadoPersonas()
        {
            return _listadoPersonasRepository.obtenerListadoPersonas();
        }
    }
}