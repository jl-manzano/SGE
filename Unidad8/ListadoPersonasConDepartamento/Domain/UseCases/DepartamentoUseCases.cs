using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.UseCases
{
    public class DepartamentoUseCases: IDepartamentoUseCases
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        public DepartamentoUseCases(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }
        public List<Departamento> getDepartamentos()
        {
            return _departamentoRepository.getDepartamentos();
        }
        public Departamento getDepartamento(int id)
        {
            return _departamentoRepository.getDepartamento(id);
        }
        public int addDepartamento(Departamento departamento)
        {
            return _departamentoRepository.addDepartamento(departamento);
        }
        public int updateDepartamento(int id, Departamento departamento)
        {
            return _departamentoRepository.updateDepartamento(id, departamento);
        }
        public int deleteDepartamento(int id)
        {
            return _departamentoRepository.deleteDepartamento(id);
        }
    }
}
