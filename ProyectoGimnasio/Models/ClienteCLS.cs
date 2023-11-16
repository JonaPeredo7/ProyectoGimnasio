using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoGimnasio.Models
{
    public class ClienteCLS
    {
        public int IDCLIENTE { get; set; }
        [Display(Name = "NOMBRE")]
        [Required]
        [StringLength(40, ErrorMessage = "El Nombre no puede superar los 40 caracteres ")]
        public string NOMBRE { get; set; }
        [Display(Name = "APELLIDO")]
        [Required]
        [StringLength(20, ErrorMessage = "El Apellido no puede superar los 20 caracteres ")]
        public string APELLIDO { get; set; }
        [Display(Name = "FECHA CONTRATO")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FECHAINGRESO { get; set; }
        [Required]
        [Display(Name = "TIPODEPLAN")]
        public string TIPODEPLAN { get; set; }

        //public string TIPODEPLAN { get; set; }
        [Display(Name = "TIPO USUARIO")]
        public Nullable<int> IDTIPOUSUARIO { get; set; }
        [Display(Name = "SEXO")]
        [Required]
        public int IDSEXO { get; set; }
        public Nullable<int> HABILITADO { get; set; }
        public Nullable<int> TIENEUSUARIO { get; set; }
        public string TIPOUSUARIO { get; set; }
        public Nullable<int> IDPLAN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AsistenciaAClase> AsistenciaAClase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opinion> Opinion { get; set; }
        public virtual PlanMembresia PlanMembresia { get; set; }
        public virtual Sexo Sexo { get; set; }
        public virtual TipoUsuario TipoUsuario1 { get; set; }
    }
}