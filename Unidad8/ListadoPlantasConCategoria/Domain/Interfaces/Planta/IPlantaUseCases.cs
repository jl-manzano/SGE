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
        ListadoPlantasWithListadoCategoriasDTO getPlantasWithListadoCategoriasDTO(int idCategoria);
        List<PlantaWithNombreCategoriaDTO> getPlantas(string categoria);
        PlantaWithNombreCategoriaDTO getPlanta(int id);
        int asignarPrecio(int id, double precio);
    }
}
