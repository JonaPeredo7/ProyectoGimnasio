using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGimnasio.Models
{
    public class OpnionCLS
    {
        public int IDOPINION { get; set; }
        public int IDCLIENTE { get; set; }
        public string COMENTARIO { get; set; }
        public Nullable<int> PUNTUACION { get; set; }
        public Nullable<System.DateTime> FECHAOPINION { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}