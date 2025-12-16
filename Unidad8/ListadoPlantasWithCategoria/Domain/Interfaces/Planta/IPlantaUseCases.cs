using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPlantaUseCases
    {
        ListadoCategoriasWithListadoPlantasPorCategoria getListadoCategoriasWithListadoPlantasPorCategoria(int? idCategoria);
        PlantaWithNombreCategoriaDTO getPlantaById(int id);
        int editarPrecio(int id, double nuevoPrecio);

    }
}
