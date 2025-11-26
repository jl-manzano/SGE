using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDepartamentoRepository
    {
        List<Departamento> getDepartamentos();
        Departamento getDepartamento(int id);
        int addDepartamento(Departamento departamento);
        int updateDepartamento(int id, Departamento departamento);
        int deleteDepartamento(int id);

    }
}
