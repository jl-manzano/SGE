using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ListadoPlantasWithListadoCategoriasDTO
    {
        public List<Planta> Plantas { get; }
        public List<Categoria> Categorias { get; }

        public ListadoPlantasWithListadoCategoriasDTO(List<Planta> plantas, List<Categoria> categorias)
        {
            Plantas = plantas;
            Categorias = categorias;
        }
    }
}
