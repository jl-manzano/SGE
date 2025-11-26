using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.UseCases
{
    public class PersonaUseCases: IPersonaUseCases
    {
        private readonly IPersonaRepository _personaRepository;
        public PersonaUseCases(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public List<Persona> getPersonas()
        {
            return _personaRepository.getPersonas();
        }

        public Persona getPersona(int id)
        {
            return _personaRepository.getPersona(id);
        }

        public int addPersona(Persona persona)
        {
            return _personaRepository.addPersona(persona);
        }

        public int updatePersona(int id, Persona persona)
        {
            return _personaRepository.updatePersona(id, persona);
        }

        public int deletePersona(int id)
        {
            return _personaRepository.deletePersona(id);
        }

    }
}
