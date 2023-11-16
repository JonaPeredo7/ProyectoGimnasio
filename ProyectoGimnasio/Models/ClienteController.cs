using System;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ProyectoGimnasio.Models
{
    public class ClienteController : Controller
    {
        //[HttpGet]
        //public ActionResult Registro()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Registro(UsuarioCLS usuario)
        //{
        //    using (var db = new Entities())
        //    {
        //        // Verificar si el nombre de usuario ya está en uso
        //        var usuarioExistente = db.Usuarios.FirstOrDefault(u => u.Usuario == usuario.Usuario);
        //        if (usuarioExistente != null)
        //        {
        //            ModelState.AddModelError("Usuario", "El nombre de usuario ya está en uso.");
        //            return View(usuario);
        //        }

        //        // Crear un nuevo objeto Usuario basado en los datos del formulario
        //        var nuevoUsuario = new Usuarios
        //        {
        //            Usuario = usuario.Usuario,
        //            Contraseña = usuario.Contraseña,
        //            // Agrega más propiedades según sea necesario
        //        };

        //        // Agregar el nuevo usuario a la base de datos
        //        db.Usuarios.Add(nuevoUsuario);
        //        db.SaveChanges();

        //        // Redirigir a la página de inicio de sesión u otra página apropiada
        //        return RedirectToAction("Login", "Cliente");
        //    }
        //}

        //[HttpGet]
        //public ActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Login(UsuarioCLS objUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var db = new Entities())
        //        {
        //            var obj = db.Usuarios.Where(a => a.Usuario.Equals(objUser.Usuario) && a.Contraseña.Equals(objUser.Contraseña)).FirstOrDefault();


        //            if (obj != null)
        //            {
        //                // Guardar información de sesión
        //                Session["UserId"] = obj.IdUsuario.ToString();
        //                Session["UserName"] = obj.Usuario.ToString();

        //                return RedirectToAction("Index", "Cliente");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "El usuario no existe.");
        //                return View();
        //            }
        //        }
        //    }
        //    return View(objUser);
        //}




        //public ActionResult Logout()
        //{
        //    // Borrar información de sesión
        //    Session.Abandon();
        //    Session.Clear();

        //    return RedirectToAction("Login");
        //}



        public ActionResult Filtrar(string filtro)
        {
            List<ClienteCLS> listaEmpleados = null;
            using (var db = new Entities())
            {
                listaEmpleados = (from cliente in db.Cliente
                                  where cliente.HABILITADO == 1 &&
                                        (string.IsNullOrEmpty(filtro) ||
                                        cliente.NOMBRE.Contains(filtro) ||
                                        cliente.APELLIDO.Contains(filtro))
                                  select new ClienteCLS
                                  {
                                      IDCLIENTE = cliente.IDCLIENTE,
                                      NOMBRE = cliente.NOMBRE,
                                      APELLIDO = cliente.APELLIDO,
                                      FECHAINGRESO = (DateTime) cliente.FECHAINGRESO,
                                      IDPLAN = cliente.IDPLAN,
                                      IDSEXO = (int) cliente.IDSEXO
                                  }).ToList();
            }
            return View("Index", listaEmpleados);
        }



        private List<ClienteCLS> ObtenerListaClientes()
        {
            List<ClienteCLS> listaClientes = new List<ClienteCLS>();

            using (var db = new Entities()) 
            {
                // Consultar los empleados habilitados en la base de datos
                var clientes = db.Cliente.Where(e => e.HABILITADO == 1).ToList();

                // Mapear los empleados a objetos EmpleadoCLS y agregarlos a la lista
                foreach (var cliente in clientes)
                {
                    ClienteCLS clienteCLS = new ClienteCLS
                    {
                        IDCLIENTE = cliente.IDCLIENTE,
                        NOMBRE = cliente.NOMBRE,
                        APELLIDO = cliente.APELLIDO,
                        IDPLAN = cliente.IDPLAN,
                        IDSEXO =(int) cliente.IDSEXO
                    };

                    listaClientes.Add(clienteCLS);
                }
            }

            return listaClientes;
        }

        public ActionResult ObtenerCliente(int id)
        {
            using (var db = new Entities())
            {
                Cliente cliente = db.Cliente.FirstOrDefault(c => c.IDCLIENTE == id);
                if (cliente != null)
                {
                    DateTime fechaIngresoCliente = (DateTime) cliente.FECHAINGRESO;
                    DateTime fechaActual = DateTime.Now;

                    int diasDePrueba = 7;
                    int diasDePago = 30;

                    DateTime fechaDePago;

                    if (fechaActual <= fechaIngresoCliente.AddDays(diasDePrueba))
                    {
                        fechaDePago = fechaIngresoCliente.AddDays(diasDePrueba);
                    }
                    else
                    {
                        fechaDePago = fechaIngresoCliente.AddDays(diasDePrueba).AddMonths(1);
                    }

                    // Determinar el color de fondo
                    string colorFondo;
                    TimeSpan diferenciaDias = fechaDePago - fechaActual;

                    if (diferenciaDias <= TimeSpan.FromDays(1))
                    {
                        colorFondo = "red"; // 1 día o menos para el pago.
                    }
                    else if (diferenciaDias <= TimeSpan.FromDays(5))
                    {
                        colorFondo = "yellow"; // 5 días o menos para el pago.
                    }
                    else if (diferenciaDias <= TimeSpan.FromDays(7))
                    {
                        colorFondo = "orange"; // 7 días o menos para el pago.
                    }
                    else
                    {
                        colorFondo = "green"; // Más de 7 días para el pago.
                    }

                    ViewBag.ColorFondo = colorFondo;
                    ViewBag.FechaDePago = fechaDePago;

                    return View(cliente);
                }


                return HttpNotFound(); // Otra respuesta adecuada según tu lógica.
            }
        }

        [HttpPost]
        public ActionResult EnviarOpinion(OpnionCLS model)
        {
            if (ModelState.IsValid)
            {
                // Aquí debes implementar la lógica para almacenar la opinión en tu sistema.
                // Esto puede incluir guardarla en una base de datos u otro almacenamiento.

                // Por ejemplo, puedes utilizar Entity Framework para guardar la opinión en una base de datos.
                using (var db = new Entities())
                {
                    var opinion = new Opinion
                    {
                        IDCLIENTE = model.IDCLIENTE,
                        COMENTARIO = model.COMENTARIO,
                        PUNTUACION = model.PUNTUACION,
                        FECHAOPINION = DateTime.Now // Establece la fecha actual
                    };
                    db.Opinion.Add(opinion);
                    db.SaveChanges();
                }

                // Redirige al usuario a una página de confirmación o muestra un mensaje en esta página.
                ViewBag.MensajeConfirmacion = "¡Gracias por tu opinión!";

                // Limpia los datos del modelo después de un envío exitoso.
                ModelState.Clear();
                model = new OpnionCLS();
            }

            return View(model);
        }



        private string ObtenerNombrePlanMembresiaPorID(int idPlan)
        {
            using (var db = new Entities()) // Reemplaza "Entities" con el nombre de tu contexto de base de datos
            {
                var plan = db.PlanMembresia.FirstOrDefault(p => p.IDPLAN == idPlan);

                if (plan != null)
                {
                    return plan.NOMBREPLAN; // Supongo que el nombre del plan se encuentra en una propiedad llamada "NombrePlan" en tu modelo de PlanMembresia

                }
            }

            return string.Empty; // Devuelve una cadena vacía si no se encuentra el plan
        }

        public ActionResult GenerarPDF(string nombre, string apellido)
        {
            List<ClienteCLS> listaClientes = ObtenerListaClientes();

            List<ClienteCLS> listaClientesFiltrados;

            if (!string.IsNullOrEmpty(nombre) || !string.IsNullOrEmpty(apellido))
            {
                listaClientesFiltrados = listaClientes
                    .Where(e => e.NOMBRE.Contains(nombre) && e.APELLIDO.Contains(apellido))
                    .ToList();
            }
            else
            {
                listaClientesFiltrados = listaClientes;
            }

            // Crear el documento PDF
            Document doc = new Document();

            // Crear un MemoryStream para almacenar el PDF
            using (MemoryStream ms = new MemoryStream())
            {
                // Crear el escritor PDF y asociarlo al MemoryStream
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                // Abrir el documento
                doc.Open();

                // Crear el título del documento
                Paragraph titulo = new Paragraph("LISTADO DE CLIENTES");
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                // Agregar un salto de línea
                doc.Add(new Paragraph(" "));

                // Crear la tabla para los datos de los CLIENTES
                PdfPTable tabla = new PdfPTable(6); // 6 columnas para los datos de los CLIENTES (se agrega la columna para el plan de membresía)
                tabla.WidthPercentage = 100; // Establecer el ancho de la tabla al 100% del ancho de la página

                // Crear las celdas de encabezado, incluyendo una para el plan de membresía
                tabla.AddCell("ID");
                tabla.AddCell("NOMBRE");
                tabla.AddCell("APELLIDO");
                tabla.AddCell("TIPODEPLAN");
                tabla.AddCell("SEXO");
                tabla.AddCell("PLANMEMBRESIA"); // Encabezado para el plan de membresía

                // Recorrer la lista de clientes y agregar las filas a la tabla, incluyendo el plan de membresía
                foreach (ClienteCLS cliente in listaClientesFiltrados)
                {
                    tabla.AddCell(cliente.IDCLIENTE.ToString());
                    tabla.AddCell(cliente.NOMBRE);
                    tabla.AddCell(cliente.APELLIDO);

                    string nombreSexo = ObtenerNombreSexoPorID(cliente.IDSEXO);
                    tabla.AddCell(nombreSexo);

                    string nombrePlan = ObtenerNombrePlanMembresiaPorID((int) cliente.IDPLAN);
                    tabla.AddCell(nombrePlan);
                }

                // Agregar la tabla al documento
                doc.Add(tabla);

                // Cerrar el documento
                doc.Close();

                // Obtener los bytes del MemoryStream
                byte[] buffer = ms.ToArray();

                // Devolver el archivo PDF como resultado de la acción
                return File(buffer, "application/pdf");
            }
        }

        private string ObtenerNombreSexoPorID(int idSexo)
        {
            using (var db = new Entities()) // Reemplaza "Entities" con el nombre de tu contexto de base de datos
            {
                var sexo = db.Sexo.FirstOrDefault(s => s.IDSEXO == idSexo);

                if (sexo != null)
                {
                    return sexo.DESCRIPCION;
                }
            }

            return string.Empty; // Devuelve una cadena vacía si no se encuentra el sexo
        }


        public ActionResult Eliminados()
        {
            List<ClienteCLS> clientesEliminados = ObtenerClientesEliminados();
            return View(clientesEliminados);
        }

        [HttpPost]
        public ActionResult Restaurar(int id)
        {
            using (var db = new Entities())
            {
                Cliente clienteRestaurar = db.Cliente.Find(id);
                if (clienteRestaurar != null)
                {
                    clienteRestaurar.HABILITADO = 1; // Restaurar al cliente estableciendo HABILITADO en 1
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Eliminados");
        }

        private List<ClienteCLS> ObtenerClientesEliminados()
        {
            List<ClienteCLS> clientesEliminados;

            using (var db = new Entities())
            {
                clientesEliminados = db.Cliente
                    .Where(e => e.HABILITADO == 0) // Obtener clientes eliminados (HABILITADO = 0)
                    .Select(e => new ClienteCLS
                    {
                        IDCLIENTE = e.IDCLIENTE,
                        NOMBRE = e.NOMBRE,
                        APELLIDO = e.APELLIDO,
                        FECHAINGRESO =(DateTime) e.FECHAINGRESO,
                        IDPLAN = e.IDPLAN,
                        IDSEXO =(int) e.IDSEXO
                    })
                    .ToList();
            }

            return clientesEliminados;
        }


        public ActionResult Index()
        {
            List<ClienteCLS> listaClientes = ObtenerListaClientes();
            using (var db = new Entities())
            {
                listaClientes = (from cliente in db.Cliente
                                 where cliente.HABILITADO == 1
                                 select new ClienteCLS
                                 {
                                     IDCLIENTE = cliente.IDCLIENTE,
                                     NOMBRE = cliente.NOMBRE,
                                     APELLIDO = cliente.APELLIDO,
                                     FECHAINGRESO = (DateTime) cliente.FECHAINGRESO,
                                     TIPODEPLAN = cliente.TIPODEPLAN,
                                     IDSEXO = (int) cliente.IDSEXO,

                                 }).ToList();
            }
            return View(listaClientes);

        }
        List<SelectListItem> listaSexo;

        private void llenarSexo()
        {
            using (var db = new Entities())
            {
                listaSexo = (from sexo in db.Sexo
                             where sexo.HABILITADO == 1
                             select new SelectListItem
                             {

                                 Text = sexo.DESCRIPCION,
                                 Value = sexo.IDSEXO.ToString()
                             }).ToList();
                listaSexo.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });


            }
        }


        [HttpGet]
        public ActionResult Agregar()
        {
            llenarSexo();
            ViewBag.lista = listaSexo ?? new List<SelectListItem>();
            return View();

        }

        [HttpPost]
        public ActionResult Agregar(ClienteCLS oClienteCLS)
        {
            if (!ModelState.IsValid)
            {
                llenarSexo();
                ViewBag.lista = listaSexo;
                return View(oClienteCLS);
            }
            else
            {
                using (var db = new Entities())
                {
                    Cliente oCliente = new Cliente();
                    oCliente.NOMBRE = oClienteCLS.NOMBRE;
                    oCliente.APELLIDO = oClienteCLS.APELLIDO;
                    oCliente.FECHAINGRESO = oClienteCLS.FECHAINGRESO;
                    oCliente.TIPODEPLAN = oClienteCLS.TIPODEPLAN;
                    oCliente.IDSEXO = oClienteCLS.IDSEXO;
                    oCliente.HABILITADO = 1;
                    db.Cliente.Add(oCliente);
                    db.SaveChanges();

                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult editar(int id)
        {

            llenarSexo();
            ViewBag.lista = listaSexo;
            ClienteCLS oClienteCLS = new ClienteCLS();
            using (var db = new Entities())
            {
                Cliente oCliente = db.Cliente.Where(p => p.IDCLIENTE.Equals(id)).First();
                oClienteCLS.IDCLIENTE = oCliente.IDCLIENTE;
                oClienteCLS.NOMBRE = oCliente.NOMBRE;
                oClienteCLS.APELLIDO = oCliente.APELLIDO;
                oClienteCLS.FECHAINGRESO = (DateTime)oCliente.FECHAINGRESO;
                oClienteCLS.TIPODEPLAN = oCliente.TIPODEPLAN;
                oClienteCLS.IDSEXO = (int)oCliente.IDSEXO;

            }
            return View(oClienteCLS);
        }

        [HttpPost]
        public ActionResult editar(ClienteCLS oClienteCLS)
        {
            int id = oClienteCLS.IDCLIENTE;
            if (!ModelState.IsValid)
            {
                llenarSexo();
                ViewBag.lista = listaSexo;
                return View(oClienteCLS);
            }
            else
            {
                using (var db = new Entities())
                {
                    Cliente oCliente = db.Cliente.Where(p => p.IDCLIENTE.Equals(id)).First();
                    oCliente.NOMBRE = oClienteCLS.NOMBRE;
                    oCliente.APELLIDO = oClienteCLS.APELLIDO;
                    oCliente.FECHAINGRESO = oClienteCLS.FECHAINGRESO;
                    oCliente.TIPODEPLAN = oClienteCLS.TIPODEPLAN;
                    oCliente.IDSEXO = oClienteCLS.IDSEXO;
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult Eliminar(int id)
        {
            using (var db = new Entities())
            {
                Cliente oCliente = db.Cliente.Where(p => p.IDCLIENTE.Equals(id)).First();
                oCliente.HABILITADO = 0;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
