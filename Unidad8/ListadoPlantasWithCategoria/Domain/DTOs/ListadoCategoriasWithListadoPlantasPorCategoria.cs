using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.DTOs
{
    public class ListadoCategoriasWithListadoPlantasPorCategoria
    {
        public List<Planta> plantas { get; }
        public List<Categoria> categorias { get; }

        public ListadoCategoriasWithListadoPlantasPorCategoria(List<Categoria> categorias)
        {
            this.categorias = categorias;
        }
        public ListadoCategoriasWithListadoPlantasPorCategoria(List<Categoria> categorias, List<Planta> plantas)
        {
            this.categorias = categorias;
            this.plantas = plantas;
        }

    }
}
