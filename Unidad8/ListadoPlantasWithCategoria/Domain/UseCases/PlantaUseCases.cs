using Domain.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.UseCases
{
    public class PlantaUseCases : IPlantaUseCases
    {
        private readonly IPlantaRepository _plantaRepository;
        private readonly ICategoriaUseCases _categoriaUseCases;

        public PlantaUseCases(IPlantaRepository plantaRepository, ICategoriaUseCases categoriaUseCases)
        {
            _plantaRepository = plantaRepository;
            _categoriaUseCases = categoriaUseCases;
        }

        public ListadoCategoriasWithListadoPlantasPorCategoria getListadoCategoriasWithListadoPlantasPorCategoria(int? idCategoria)
        {
            // Siempre obtenemos todas las categorías para el select
            var categorias = _categoriaUseCases.getCategorias();

            // Si no se seleccionó categoría, solo devolvemos las categorías
            if (idCategoria == null || idCategoria == 0)
            {
                return new ListadoCategoriasWithListadoPlantasPorCategoria(categorias);
            }
            else
            {
                // Si se seleccionó una categoría, filtramos las plantas
                var todasLasPlantas = _plantaRepository.getPlantas();
                var plantasFiltradas = new List<Planta>();

                // Recorremos todas las plantas y solo añadimos las de la categoría seleccionada
                foreach (var planta in todasLasPlantas)
                {
                    if (planta.IdCategoria == idCategoria)
                    {
                        plantasFiltradas.Add(planta);
                    }
                }

                return new ListadoCategoriasWithListadoPlantasPorCategoria(categorias, plantasFiltradas);
            }
        }
        public PlantaWithNombreCategoriaDTO getPlantaById(int id)
        {
            var planta = _plantaRepository.getPlantaById(id);
            var categoria = _categoriaUseCases.getNombreCategoriaById(planta.IdCategoria);
            return new PlantaWithNombreCategoriaDTO(planta, categoria);
        }

        private bool compruebaPrecio(int id, double precioNuevo)
        {
            // Obtener la planta actual
            var planta = _plantaRepository.getPlantaById(id);

            // Comprobar si el precio nuevo es mayor al anterior
            bool esValido = false;

            if (precioNuevo > planta.Precio)
            {
                esValido = true;
            }

            return esValido;
        }

        public int editarPrecio(int id, double nuevoPrecio)
        {
            int resultado = 0;

            // Comprobar si el precio es válido antes de editar
            if (compruebaPrecio(id, nuevoPrecio))
            {
                resultado = _plantaRepository.editarPrecio(id, nuevoPrecio);
            }

            return resultado;
        }
    }
}