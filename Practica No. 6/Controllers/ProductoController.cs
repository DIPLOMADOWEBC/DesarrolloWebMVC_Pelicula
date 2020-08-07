using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practica_No._6.Models;

namespace Practica_No._6.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            RegistroProducto rp = new RegistroProducto();

            return View(rp.RecuperarTodos());
        }

        public ActionResult Grabar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Grabar(FormCollection collection)
        {
            try
            {

                RegistroProducto rp = new RegistroProducto();
                Producto prod = new Producto
                {
                    //Codigo = int.Parse(collection["Codigo"]),
                    Descripcion = collection["Descripcion"],
                    Tipo = collection["Tipo"],
                    Precio = double.Parse(collection["Precio"].ToString()),
                };
                rp.GrabarProducto(prod);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
                //ModelState.AddModelError("", ex.Message + " " + ex.StackTrace);
                //return View("Error", new HandleErrorInfo(ex, "Home", "Grabar"));
            }
        }

        public ActionResult Borrar(int cod)
        {
            RegistroProducto prod = new RegistroProducto();
            prod.Borrar(cod);
            return RedirectToAction("Index");
        }

        public ActionResult Modificacion(int cod)
        {
            RegistroProducto prod = new RegistroProducto();
            Producto rpt = prod.Recuperar(cod);
            return View(rpt);
        }

        [HttpPost]
        public ActionResult Modificacion(FormCollection collection)
        {
            RegistroProducto prod = new RegistroProducto();
            Producto rpt = new Producto()
            {
                Id = int.Parse(collection["Id"]),
                Descripcion = collection["Descripcion"],
                Tipo = collection["Tipo"],
                Precio = double.Parse(collection["Precio"].ToString()),
            };

            prod.Modificar(rpt);
            return RedirectToAction("Index");
        }
    }
}