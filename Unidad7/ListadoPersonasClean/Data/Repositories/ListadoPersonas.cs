using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.UseCases.Interfaces;

namespace Data.Repositories
{
    public class ListadoPersonas : IListadoPersonasRepository
    {
        private List<Persona> _listadoPersonas;

        public ListadoPersonas()
        {
            _listadoPersonas = new List<Persona>()
            {
                new Persona(1, "Juan", "Perez", new DateTime(1991, 5, 15), "Calle Falsa 123", "555-1234", ""),
                new Persona(2, "Maria", "Gomez", new DateTime(1996, 7, 10), "Avenida Libertad 456", "555-5678", ""),
                new Persona(3, "Carlos", "Sanchez", new DateTime(1981, 3, 20), "Plaza Mayor 789", "555-9876", "")
            };
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