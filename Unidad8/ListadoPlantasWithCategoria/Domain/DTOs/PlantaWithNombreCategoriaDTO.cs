using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.DTOs
{
    public class PlantaWithNombreCategoriaDTO
    {
        public Planta planta { get; }
        public string nombreCategoria { get; }

        public PlantaWithNombreCategoriaDTO(Planta planta, string nombreCategoria)
        {
            this.planta = planta;
            this.nombreCategoria = nombreCategoria;
        }

    }
}
