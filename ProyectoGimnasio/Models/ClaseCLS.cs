using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGimnasio.Models
{
    public class ClaseCLS
    {
        public int IDCLASE { get; set; }
        public string NOMBRECLASE { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<System.TimeSpan> HORARIO { get; set; }
        public int HABILITADO { get; set; }
        public Nullable<int> IDTurno { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AsistenciaAClase> AsistenciaAClase { get; set; }
        public virtual Turno Turno { get; set; }
    }
}