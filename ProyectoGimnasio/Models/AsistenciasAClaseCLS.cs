using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGimnasio.Models
{
    public class AsistenciasAClaseCLS
    {
        public int IDASISTENCIA { get; set; }
        public int IDCLIENTE { get; set; }
        public int IDCLASE { get; set; }
        public Nullable<System.DateTime> FECHAASISTENCIA { get; set; }

        public virtual Clase Clase { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}