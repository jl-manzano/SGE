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
        public Planta Planta { get; }
        public string NombreCategoria { get; }

        public PlantaWithNombreCategoriaDTO(Planta planta, string nombreCategoria)
        {
            Planta = planta;
            NombreCategoria = nombreCategoria;
        }
    }
}
