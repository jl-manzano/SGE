using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ListadoMisiones : IListadoMisionesRepository
    {
        private List<Mision> _listadoMisiones;

        public ListadoMisiones() 
        { 
            _listadoMisiones = new List<Mision>()
            {
                new Mision(1, "Rescate de Baby Yoda",
                           "Debes hacerte con Grogu y llevárselo a Luke Skywalker para su entrenamiento.",
                            5000),
                
                new Mision(2, "Recuperar armadura Beskar",
                            "Tu armadura de Beskar ha sido robada. Debes encontrarla.",
                            2000),
                
                new Mision(3, "Planeta Sorgon",
                            "Debes llevar a un niño de vuelta a su planeta natal 'Sorgon'.",
                            500),
                
                new Mision(4, "Renacuajos",
                            "Debes llevar a una Dama Rana y sus huevos de Tatooine a la luna del estuario Trask, " +
                            "donde su esposo fertilizará los huevos.",
                            500)
            };


        }

        public List<Mision> obtenerListadoMisiones()
        {
            return _listadoMisiones;
        }

    }
}
