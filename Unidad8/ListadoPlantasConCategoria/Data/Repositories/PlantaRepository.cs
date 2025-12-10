using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PlantaRepository : IPlantaRepository
    {
        // Usamos una lista en memoria para simular la base de datos
        private static List<Planta> _plantas = new List<Planta>
        {
            new Planta(1, "Planta A", "Descripción A", 10.0, 1),
            new Planta(2, "Planta B", "Descripción B", 15.0, 1),
            new Planta(3, "Planta C", "Descripción C", 20.0, 2),
            new Planta(4, "Planta D", "Descripción D", 25.0, 2)
        };

        // Simulamos el repositorio de plantas sin usar DbContext

        /// <summary>
        /// Obtiene las plantas asociadas a una categoría específica.
        /// </summary>
        public List<Planta> getPlantas(string categoria)
        {
            // Filtra las plantas por categoría (usando IdCategoria)
            return _plantas.Where(p => p.IdCategoria.ToString() == categoria).ToList();
        }

        /// <summary>
        /// Obtiene una planta específica por su ID.
        /// </summary>
        public Planta getPlanta(int id)
        {
            // Busca la planta por su ID
            return _plantas.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Asigna un nuevo precio a una planta.
        /// </summary>
        public int asignarPrecio(int id, double precio)
        {
            // Busca la planta por ID y asigna el nuevo precio
            Planta planta = _plantas.FirstOrDefault(p => p.Id == id);
            if (planta != null)
            {
                planta.Precio = precio;
                // Simulamos que se guardan los cambios (aquí no hay base de datos real)
                return 1;  // Retornamos 1 para simular que la actualización fue exitosa
            }
            return 0;  // Retorna 0 si no encontró la planta
        }
    }
}
