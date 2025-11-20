using Domain.UseCases.Interfaces;
using System.Collections.Generic;

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

        public Entities.Persona obtenerPersonaId(int id)
        {
            return _listadoPersonasRepository.obtenerPersonaId(id);
        }

        public void insertarPersona(Entities.Persona persona)
        {
            _listadoPersonasRepository.insertarPersona(persona);
        }

        public void actualizarPersona(Entities.Persona persona)
        {
            _listadoPersonasRepository.actualizarPersona(persona);
        }

        public void eliminarPersona(int id)
        {
            _listadoPersonasRepository.eliminarPersona(id);
        }
    }
}
