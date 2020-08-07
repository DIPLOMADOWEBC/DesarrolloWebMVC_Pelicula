using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practica_No._6.Models;

namespace Practica_No._6.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            RegistroPelicula rp = new RegistroPelicula();

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

                RegistroPelicula rp = new RegistroPelicula();
                Pelicula peli = new Pelicula
                {
                    //Codigo = int.Parse(collection["Codigo"]),
                    Titulo = collection["Titulo"],
                    Director = collection["Director"],
                    AutorPrincipal = collection["AutorPrincipal"],
                    numAutores = int.Parse(collection["numActores"]),
                    Duracion = double.Parse(collection["Duracion"].ToString()),
                    Estreno = int.Parse(collection["Estreno"])
                };
                rp.GrabarPelicula(peli);
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
            RegistroPelicula peli = new RegistroPelicula();
            peli.Borrar(cod);
            return RedirectToAction("Index");
        }

        public ActionResult Modificacion(int cod)
        {
            RegistroPelicula peli = new RegistroPelicula();
            Pelicula rpt = peli.Recuperar(cod);
            return View(rpt);
        }

        [HttpPost]
        public ActionResult Modificacion(FormCollection collection)
        {
            RegistroPelicula peli = new RegistroPelicula();
            Pelicula rpt = new Pelicula()
            {
                Codigo = int.Parse(collection["Codigo"]),
                Titulo = collection["Titulo"],
                Director = collection["Director"],
                AutorPrincipal = collection["AutorPrincipal"],
                numAutores = int.Parse(collection["numActores"]),
                Duracion = double.Parse(collection["Duracion"].ToString()),
                Estreno = int.Parse(collection["Estreno"])
            };

            peli.Modificar(rpt);
            return RedirectToAction("Index");
        }
    }
}