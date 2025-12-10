using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases
{
    public class PlantaUseCases : IPlantaUseCases
    {
        private readonly IPlantaRepository _plantaRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        // Constructor que inicializa los repositorios de plantas y categorías
        public PlantaUseCases(IPlantaRepository plantaRepository, ICategoriaRepository categoriaRepository)
        {
            _plantaRepository = plantaRepository;
            _categoriaRepository = categoriaRepository;
        }

        /// <summary>
        /// Obtiene las plantas de una categoría específica junto con todas las categorías.
        /// </summary>
        public ListadoPlantasWithListadoCategoriasDTO getPlantasWithListadoCategoriasDTO(int idCategoria)
        {
            List<Planta> plantas = _plantaRepository.getPlantas(idCategoria.ToString());
            List<Categoria> categorias = _categoriaRepository.getCategorias();

            // Devuelve el DTO combinado con las plantas y las categorías
            return new ListadoPlantasWithListadoCategoriasDTO(plantas, categorias);
        }

        /// <summary>
        /// Obtiene todas las plantas de una categoría.
        /// </summary>
        public List<PlantaWithNombreCategoriaDTO> getPlantas(string categoria)
        {
            List<Planta> plantas = _plantaRepository.getPlantas(categoria);
            Categoria categoriaObj = _categoriaRepository.getCategoria(int.Parse(categoria));
            string categoriaNombre = categoriaObj.Nombre;

            List<PlantaWithNombreCategoriaDTO> plantasDTO = new List<PlantaWithNombreCategoriaDTO>();
            foreach (Planta planta in plantas)
            {
                PlantaWithNombreCategoriaDTO plantaDTO = new PlantaWithNombreCategoriaDTO(planta, categoriaNombre);
                plantasDTO.Add(plantaDTO);
            }

            return plantasDTO;
        }

        /// <summary>
        /// Obtiene una planta específica con su nombre de categoría.
        /// </summary>
        public PlantaWithNombreCategoriaDTO getPlanta(int id)
        {
            Planta planta = _plantaRepository.getPlanta(id);
            Categoria categoria = _categoriaRepository.getCategoria(planta.IdCategoria);
            return new PlantaWithNombreCategoriaDTO(planta, categoria.Nombre);
        }

        /// <summary>
        /// Asigna un precio a una planta si el nuevo precio es mayor que el precio actual.
        /// </summary>
        public int asignarPrecio(int id, double precio)
        {
            return _plantaRepository.asignarPrecio(id, precio);
        }

        /// <summary>
        /// Verifica si el precio de la planta puede ser actualizado.
        /// </summary>
        private bool compruebaPrecio(int id, double precioNuevo)
        {
            Planta planta = _plantaRepository.getPlanta(id);
            return planta.Precio < precioNuevo;
        }
    }
}
