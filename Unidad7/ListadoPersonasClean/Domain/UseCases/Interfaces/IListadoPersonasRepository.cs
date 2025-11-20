using Domain.Entities;
using System.Collections.Generic;

namespace Domain.UseCases.Interfaces
{
    public interface IListadoPersonasRepository
    {
        List<Persona> obtenerListadoPersonas();
        Persona obtenerPersonaId(int id);

        void insertarPersona(Persona persona);
        void actualizarPersona(Persona persona);
        void eliminarPersona(int id);
    }
}
