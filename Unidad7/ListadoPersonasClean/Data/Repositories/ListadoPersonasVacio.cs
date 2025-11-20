using Domain.UseCases.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ListadoPersonasVacio : IListadoPersonasRepository
    {
        private List<Persona> _listadoPersonas;

        public ListadoPersonasVacio()
        {
            _listadoPersonas = new List<Persona>();
        }

        public List<Persona> obtenerListadoPersonas()
        {
            return _listadoPersonas;
        }

        public Persona obtenerPersonaId(int id)
        {
            return _listadoPersonas.FirstOrDefault(p => p.Id == id);
        }

        public void insertarPersona(Persona persona)
        {
            _listadoPersonas.Add(persona);
        }

        public void actualizarPersona(Persona persona)
        {
            var personaExistente = obtenerPersonaId(persona.Id);
            if (personaExistente != null)
            {
                personaExistente.Nombre = persona.Nombre;
                personaExistente.Apellidos = persona.Apellidos;
                personaExistente.FechaNac = persona.FechaNac;
                personaExistente.Direccion = persona.Direccion;
                personaExistente.Telefono = persona.Telefono;
                personaExistente.Foto = persona.Foto;
            }
        }

        public void eliminarPersona(int id)
        {
            var personaAEliminar = obtenerPersonaId(id);
            if (personaAEliminar != null)
            {
                _listadoPersonas.Remove(personaAEliminar);
            }
        }

    }
}