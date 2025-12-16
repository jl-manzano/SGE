using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPlantaRepository
    {
        List<Planta> getPlantas();
        Planta getPlantaById(int id);
        int editarPrecio(int id, double nuevoPrecio);
    }
}
