using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGimnasio.Models
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {

            List<RolCLS> listaRol = new List<RolCLS>();
            using (var db = new Entities())
            {
                listaRol = (from rol in db.Rol

                            select new RolCLS
                            {
                                IDRol = rol.IDRol,
                                NOMBRE = rol.NOMBRE,
                                DESCRIPCION = rol.DESCRIPCION,
                                HABILITADO = (int)rol.HABILITADO
                            }).ToList();
            }
            return View(listaRol);

        }


        public ActionResult Filtrar(RolCLS oRolCLS)
        {
            string nombreRol = oRolCLS.nombreFiltro;
            List<RolCLS> listaRol = new List<RolCLS>();
            using (var db = new Entities())
            {
                if (nombreRol == null)
                {
                    listaRol = (from rol in db.Rol
                                select new RolCLS
                                {
                                    IDRol = rol.IDRol,
                                    NOMBRE = rol.NOMBRE,
                                    DESCRIPCION = rol.DESCRIPCION
                                }).ToList();
                }
                else
                {
                    listaRol = (from rol in db.Rol
                                where rol.NOMBRE.Contains(nombreRol)
                                select new RolCLS
                                {
                                    IDRol = rol.IDRol,
                                    NOMBRE = rol.NOMBRE,
                                    DESCRIPCION = rol.DESCRIPCION
                                }).ToList();
                }

            }
            return PartialView("_TablaRol", listaRol);
        }

        public int Guardar(RolCLS oRolCLS, int titulo)
        {
            int respuesta = 0;


            using (var db = new Entities())
            {
                if (titulo == 1)
                {
                    Rol oRol = new Rol();
                    oRol.IDRol = oRolCLS.IDRol;
                    oRol.NOMBRE = oRolCLS.NOMBRE;
                    oRol.DESCRIPCION = oRolCLS.DESCRIPCION;
                    oRol.HABILITADO = 1;
                    db.Rol.Add(oRol);
                    respuesta = db.SaveChanges();
                }
            }
            return respuesta;
        }

        public string Guardar2(RolCLS oRolCLS, int titulo)//Guardar o editar
        {
            string rpta = "";

            try
            {
                if (!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class = 'list-group'>";
                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item'>" + item + "</li>";
                    }
                    rpta += "</ul>";
                }
                else
                {
                    using (var db = new Entities())
                    {
                        if (titulo == -1)
                        {
                            Rol oRol = new Rol();
                            oRol.IDRol = oRolCLS.IDRol;
                            oRol.NOMBRE = oRolCLS.NOMBRE;
                            oRol.DESCRIPCION = oRolCLS.DESCRIPCION;
                            oRol.HABILITADO = 1;
                            db.Rol.Add(oRol);
                            rpta = db.SaveChanges().ToString();
                            if (rpta == "0") rpta = "";
                        }
                        else
                        {
                            Rol oRol = db.Rol.Where(p => p.IDRol == titulo).First();
                            oRol.NOMBRE = oRolCLS.NOMBRE;
                            oRol.DESCRIPCION = oRolCLS.DESCRIPCION;
                            rpta = db.SaveChanges().ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = "";
            }

            return rpta;
        }

        public string Eliminar(RolCLS oRolCLS)
        {
            string respuesta = "";

            try
            {
                int id = oRolCLS.IDRol;
                using (var db = new Entities())
                {
                    Rol oRol = db.Rol.Where(p => p.IDRol == id).First();
                    db.Rol.Remove(oRol);
                    respuesta = db.SaveChanges().ToString();
                }
            }
            catch (Exception)
            {
                respuesta = "";
            }
            return respuesta;
        }

        public JsonResult RellenarCampos(int titulo)
        {
            RolCLS oRolCLS = new RolCLS();
            using (var db = new Entities())
            {
                Rol oRol = db.Rol.Where(p => p.IDRol == titulo).First();
                oRolCLS.NOMBRE = oRol.NOMBRE;
                oRolCLS.DESCRIPCION = oRol.DESCRIPCION;
                oRolCLS.HABILITADO = 1;
            }
            return Json(oRolCLS, JsonRequestBehavior.AllowGet);
        }
    }
}
