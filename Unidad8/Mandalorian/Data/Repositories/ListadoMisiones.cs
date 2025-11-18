using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ListadoMisiones : IListadoMisiones
    {
        private List<Mision> misiones = new List<Mision>
        {
                    
            new Mision(1, "Rescate de Baby Yoda",
                        "Debes hacerte con Grogu y llevárselo a Luke SkyWalker para su entrenamiento.",
                        5000),

            new Mision(2, "Recuperar armadura Beskar",
                        "Tu armadura de Beskar ha sido robada. Debes encontrarla.",
                        2000),

            new Mision(3, "Planeta Sorgon",
                        "Debes llevar a un niño de vuelta a su planeta natal “Sorgon”.",
                        500),

            new Mision(4, "Renacuajos",
                        "Debes llevar a una Dama Rana y sus huevos de Tatooine a la luna del estuario Trask, donde su esposo fertilizará los huevos.",
                        500)
        };


        public List<Mision> getMisiones()
        {
            return misiones;
        }

    }
}
