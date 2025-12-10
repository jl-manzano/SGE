using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPlantaRepository
    {
        List<Planta> getPlantas(string categoria);
        Planta getPlanta(int id);
        int asignarPrecio(int id, double precio);
    }
}
