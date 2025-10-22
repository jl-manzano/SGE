using Ejercicio1.Models.Entities;
using System.Collections.Generic;

namespace Ejercicio1.Models.Entities
{
    public class ListadoPersonas
    {
        // Lista de personas
        public static List<Persona> personas = new List<Persona>
            {
                new Persona(1, "José Manuel Maya", "Maya Hidalgo", 20),
                new Persona(2, "Lucía García", "López Galiana", 30),
                new Persona(3, "Carlos Fernández", "García Toral", 40),
                new Persona(4, "María López", "Mapher Game", 15),
                new Persona(5, "Antonio Ruiz", "En PlayStore", 35)
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