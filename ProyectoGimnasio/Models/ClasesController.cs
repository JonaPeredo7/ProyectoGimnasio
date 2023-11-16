using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGimnasio.Models
{
    public class ClaseController : Controller
    {
        private Entities db = new Entities(); // Reemplaza 'Entities' con tu contexto de base de datos real

        public ActionResult Index()
        {
            List<Clase> listaClases = db.Clase.ToList(); // Obtén la lista de clases desde la base de datos
            return View(listaClases);
        }

        // Otras acciones como Agregar, Detalles, Editar y Eliminar deberían ir aquí

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
