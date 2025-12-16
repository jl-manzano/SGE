using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class PlantaRepository: IPlantaRepository
    {
        private static List<Planta> _plantas = new List<Planta>
        {
            // Categoría 1
            new Planta(1, "Rosa Roja", "Rosa clásica de pétalos rojos intensos", 12.50, 1),
            new Planta(2, "Tulipán Amarillo", "Tulipán vibrante ideal para jardines", 8.99, 1),
            new Planta(3, "Girasol", "Planta solar de gran tamaño y flores amarillas", 15.00, 1),
            new Planta(4, "Margarita Blanca", "Flores blancas con centro amarillo", 6.50, 1),
            new Planta(5, "Lirio Naranja", "Lirio aromático de color naranja brillante", 18.75, 1),
            
            // Categoría 2
            new Planta(6, "Cactus Redondo", "Cactus de fácil cuidado para interiores", 10.00, 2),
            new Planta(7, "Aloe Vera", "Planta medicinal con propiedades curativas", 14.50, 2),
            new Planta(8, "Suculenta Mini", "Pequeña suculenta decorativa", 5.99, 2),
            new Planta(9, "Cactus Columnar", "Cactus alto de crecimiento vertical", 22.00, 2),
            new Planta(10, "Echeveria", "Suculenta en forma de roseta", 9.75, 2),
            new Planta(11, "Crassula Ovata", "Árbol de Jade, planta de la suerte", 16.50, 2),
            
            // Categoría 3
            new Planta(12, "Helecho Boston", "Helecho frondoso ideal para interiores", 13.99, 3),
            new Planta(13, "Potos Dorado", "Planta trepadora de hojas verdes", 11.50, 3),
            new Planta(14, "Monstera Deliciosa", "Planta tropical de hojas grandes", 28.00, 3),
            new Planta(15, "Ficus Benjamina", "Árbol decorativo de interior", 35.50, 3),
            new Planta(16, "Sansevieria", "Lengua de suegra, purifica el aire", 17.25, 3),
            new Planta(17, "Calathea Orbifolia", "Hojas grandes con patrones únicos", 24.99, 3),
            new Planta(18, "Bambú de la Suerte", "Planta ornamental de fácil cuidado", 12.00, 3),
            new Planta(19, "Filodendro", "Planta trepadora de hojas brillantes", 19.50, 3),
            new Planta(20, "Orquídea Phalaenopsis", "Orquídea elegante con flores duraderas", 32.00, 3)
        };

        public List<Planta> getPlantas()
        {
            return _plantas;
        }

        public Planta getPlantaById(int id)
        {
            Planta plantaEncontrada = new Planta();

            foreach (var planta in _plantas)
            {
                if (planta.Id == id)
                {
                    plantaEncontrada = planta;
                }
            }

            return plantaEncontrada;
        }

        public int editarPrecio(int id, double nuevoPrecio)
        {
            int resultado = 0;

            foreach (var planta in _plantas)
            {
                if (planta.Id == id)
                {
                    planta.Precio = nuevoPrecio;
                    resultado = 1;
                }
            }

            return resultado;
        }
    }
}
