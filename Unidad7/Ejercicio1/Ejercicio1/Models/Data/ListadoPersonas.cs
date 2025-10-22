using Ejercicio1.Models.Entities;
using System.Collections.Generic;

namespace Ejercicio1.Models.Entities
{
    public class ListadoPersonas
    {
        // Lista de personas
        public static List<Persona> personas = new List<Persona>
            {
                new Persona(1, "Jos� Manuel Maya", "Maya Hidalgo", 20),
                new Persona(2, "Luc�a Garc�a", "L�pez Galiana", 30),
                new Persona(3, "Carlos Fern�ndez", "Garc�a Toral", 40),
                new Persona(4, "Mar�a L�pez", "Mapher Game", 15),
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