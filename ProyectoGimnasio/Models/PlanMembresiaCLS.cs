using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGimnasio.Models
{
    public class PlanMembresiaCLS
    {
        public int IDPLAN { get; set; }
        public string NOMBREPLAN { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal PRECIO { get; set; }
        public int HABILITADO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cliente> Cliente { get; set; }
    }
}