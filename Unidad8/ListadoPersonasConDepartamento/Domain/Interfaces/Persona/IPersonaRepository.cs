using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPersonaRepository
    {
        List<Persona> getPersonas();
        Persona getPersona(int id);
        int addPersona(Persona persona);
        int updatePersona(int id, Persona persona);
        int deletePersona(int id);
    }
}
