using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class PersonaDTO
    {
        #region Atributos privados
        private int _id;
        private string _nombre;
        private string _apellidos;
        private int? _idDepartamento;
        #endregion

        #region Propiedades públicas
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Apellidos
        {
            get { return _apellidos; }
            set { _apellidos = value; }
        }

        public int? IdDepartamento
        {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }
        #endregion

        #region Constructores
        public PersonaDTO(int id, string nombre, string apellidos, int? idDepartamento)
        {
            _id = id;
            _nombre = nombre;
            _apellidos = apellidos;
            _idDepartamento = idDepartamento;
        }
        #endregion
    }
}