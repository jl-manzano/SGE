using Ejercicio1.Models.Entities;
using System.Collections.Generic;

namespace Ejercicio1.Models.Entities
{
    public class ListadoPersonas
    {
        public static List<Persona> personas = new List<Persona>
            {
                new Persona(1, "Ana Sánchez", "Jiménez Muñoz", 28),
                new Persona(2, "Pedro Rodríguez", "Pérez Castro", 22),
                new Persona(3, "Laura Fernández", "Cano Martínez", 33),
                new Persona(4, "David Martín", "Ruiz García", 40),
                new Persona(5, "Isabel Gómez", "Martín Ramírez", 26)
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