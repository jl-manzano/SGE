using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Mision
    {
        #region atributos privados
        private int id;
        private string title;
        private string description;
        private int award;
        #endregion

        #region propiedades publicas
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int Award
        {
            get { return award; }
            set { award = value; }
        }
        #endregion

        #region constructores
        public Mision() { }

        public Mision(int id, string title, string description, int award)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.award = award;
        }
        #endregion
    }
}