using System.Collections.Generic;

namespace EjerciciosUnidad7.Models.Entities.DAL
{
	public class ListadoDepartamentos
	{
		// lista privada que contiene los departamentos
		private List<Departamento> _lista = new List<Departamento>
		{
			new Departamento(1, "Recursos Humanos"),
			new Departamento(2, "Finanzas"),
			new Departamento(3, "Marketing"),
			new Departamento(4, "Tecnología"),
			new Departamento(5, "Ventas"),
			new Departamento(6, "Logística"),
		};

		public ListadoDepartamentos()
		{
		}

		public List<Departamento> Lista
		{
			get { return _lista; }
		}
	}
}
