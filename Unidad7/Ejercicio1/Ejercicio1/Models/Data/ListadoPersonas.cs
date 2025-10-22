using Ejercicio1.Models.Entities;
using System.Collections.Generic;

namespace Ejercicio1.Models.Entities
{
    public class ListadoPersonas
    {
        public static List<Persona> personas = new List<Persona>
            {
                new Persona(1, "Ana S�nchez", "Jim�nez Mu�oz", 28),
                new Persona(2, "Pedro Rodr�guez", "P�rez Castro", 22),
                new Persona(3, "Laura Fern�ndez", "Cano Mart�nez", 33),
                new Persona(4, "David Mart�n", "Ruiz Garc�a", 40),
                new Persona(5, "Isabel G�mez", "Mart�n Ram�rez", 26)
            };

        public static List<Persona> getListadoPersonas()
        {
            return personas;
        }

        public static Persona getPersonaByPosition(int position) 
        {
            return personas[position];
        }
    }
}