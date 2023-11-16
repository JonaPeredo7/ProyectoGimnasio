using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGimnasio.Models
{
    public class RolCLS
    {
        public int IDRol { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<int> HABILITADO { get; set; }
        public string nombreFiltro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}