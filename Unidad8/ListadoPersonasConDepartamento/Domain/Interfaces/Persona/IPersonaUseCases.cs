using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPersonaUseCases
    {
        List<Departamento> getDepartamentos();
        PersonaWithNombreDepartamentoDTO getPersona(int id);
        List<PersonaWithNombreDepartamentoDTO> getPersonas();
        PersonaWithListadoDepartamentoDTO GetPersonaWithListadoDepartamento(int id);
        int addPersona(Persona persona);
        int updatePersona(int id, Persona persona);
        int deletePersona(int id);
    }
}
