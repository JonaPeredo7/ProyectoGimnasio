using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoGimnasio.Models
{
    public class UsuarioCLS
    {
        public int IdUsuario { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }
        public Nullable<int> IdTipoUsuario { get; set; }
        public Nullable<int> ID { get; set; }
        public Nullable<int> HABILITADO { get; set; }
        public Nullable<int> IDRol { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}