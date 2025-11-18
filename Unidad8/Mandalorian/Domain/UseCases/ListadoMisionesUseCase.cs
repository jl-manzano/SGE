using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.UseCases
{
    public class ListadoMisionesUseCase : IListadoMisionesUseCase
    {
        private readonly IListadoMisiones _listadoMisiones;

        public ListadoMisionesUseCase(IListadoMisiones listadoMisiones)
        {
            _listadoMisiones = listadoMisiones;
        }

        public List<Mision> getMisiones()
        {
            DateTime horaActual = DateTime.Now;

            if (horaActual.Hour <= 20)
            {
                return _listadoMisiones.getMisiones();
            }

            return new List<Mision>();
        }

        public Mision getMisionById(int id)
        {
            List<Mision> listadoMisiones = _listadoMisiones.getMisiones();
            foreach (var mision in listadoMisiones)
            {
                if (mision.Id == id)
                {
                    return mision;
                }
            }
            throw new Exception($"No se encontró ninguna misión con ID {id}");
        }


        public ListadoConMisionSeleccionadaDto getListadoConMisionSeleccionada(int idSeleccionado)
        {
            List<Mision> listadoMisiones = getMisiones();
            Mision misionSeleccionada = getMisionById(idSeleccionado);

            return new ListadoConMisionSeleccionadaDto(listadoMisiones, misionSeleccionada);
        }
    }
}