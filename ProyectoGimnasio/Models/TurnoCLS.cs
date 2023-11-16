using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGimnasio.Models
{
    public class TurnoCLS
    {
        public int IDTurno { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> CapacidadMaxima { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clase> Clase { get; set; }
    }
}