using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGimnasio.Models
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Registro()
        {
            // Comprueba si el usuario actual tiene permiso de administrador (asumiendo que el ID del rol de administrador es 24)
            if (User.IsInRole("Administrador"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("AccesoDenegado");
            }
        }

        [HttpPost]
        public ActionResult Registro(UsuarioCLS usuario)
        {
            if (!User.IsInRole("Administrador"))
            {
                return RedirectToAction("AccesoDenegado");
            }

            using (var db = new Entities())
            {
                // Verificar si el nombre de usuario ya está en uso
                var usuarioExistente = db.Usuarios.FirstOrDefault(u => u.Usuario == usuario.Usuario);
                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("Usuario", "El nombre de usuario ya está en uso.");
                    return View(usuario);
                }

                // Crear un nuevo objeto Usuario basado en los datos del formulario
                var nuevoUsuario = new Usuarios
                {
                    Usuario = usuario.Usuario,
                    Contraseña = usuario.Contraseña,
                    // Agrega más propiedades según sea necesario
                };

                // Asignar el rol de "Cliente" (asumiendo que el ID del rol de cliente es 25)
                nuevoUsuario.IDRol = 25;

                // Agregar el nuevo usuario a la base de datos
                db.Usuarios.Add(nuevoUsuario);
                db.SaveChanges();

                // Redirigir a la página de inicio de sesión u otra página apropiada
                return RedirectToAction("Login", "Usuario");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsuarioCLS objUser)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Entities())
                {
                    var obj = db.Usuarios.Where(a => a.Usuario.Equals(objUser.Usuario) && a.Contraseña.Equals(objUser.Contraseña)).FirstOrDefault();

                    if (obj != null)
                    {
                        // Guardar información de sesión
                        Session["UserId"] = obj.IdUsuario.ToString();
                        Session["UserName"] = obj.Usuario.ToString();

                        // Comprobar el rol del usuario
                        var rol = db.Rol.FirstOrDefault(r => r.IDRol == obj.IDRol);
                        if (rol != null)
                        {
                            if (rol.IDRol == 24) // ID de Administrador
                            {
                                // El usuario ha iniciado sesión como administrador
                                return RedirectToAction("Index", "Admin");
                            }
                            else if (rol.IDRol == 25) // ID de Cliente
                            {
                                // El usuario ha iniciado sesión como cliente
                                return RedirectToAction("Index", "Cliente");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "El usuario no existe.");
                        return View();
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult AccesoDenegado()
        {
            return View();
        }

        public ActionResult Logout()
        {
            // Borrar información de sesión
            Session.Abandon();
            Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
