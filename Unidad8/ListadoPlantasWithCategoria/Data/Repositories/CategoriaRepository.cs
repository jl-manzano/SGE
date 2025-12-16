using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Data.Repositories
{
    public class CategoriaRepository: ICategoriaRepository
    {
        private static List<Categoria> _categorias = new List<Categoria>
        {
            new Categoria(1, "Categoria 1"),
            new Categoria(2, "Categoria 2"),
            new Categoria(3, "Categoria 3")
        };

        public List<Categoria> getCategorias()
        {
            return _categorias;
        }

        public string getNombreCategoriaById(int id)
        {
            string categoriaEncontrada = "";

            foreach (var categoria in _categorias)
            {
                if (categoria.Id == id)
                {
                    categoriaEncontrada = categoria.Nombre;
                }
            }

            return categoriaEncontrada;
        }
    }
}
