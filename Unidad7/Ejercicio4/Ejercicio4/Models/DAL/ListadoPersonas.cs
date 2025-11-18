using System.Collections.Generic;

namespace EjerciciosUnidad7.Models.Entities.DAL
{
	public class ListadoPersonas
	{
		// lista privada que contiene las personas
		private List<Persona> _lista = new List<Persona>
		{
			new Persona(1, "Juan", "Pérez", 30, 1),
            new Persona(3, "Luis", "Martínez", 35, 3),
            new Persona(4, "Ana", "López", 27, 4),
            new Persona(5, "Carlos", "Ramírez", 40, 5),
            new Persona(6, "Laura", "Díaz", 33, 6),
        };

		public ListadoPersonas()
		{
		}

		public List<Persona> Lista
		{
			get { return _lista; }
		}
	}
}
