using Finanzas.Helpers;
using Finanzas.Models;
using Finanzas.Models.Resultados;
using Finanzas.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Finanzas.Controllers
{
    public class BonoController : Controller
    {
        // GET: Bono
        public ActionResult Index()
        {
            return View(SessionHelper.User);
        }

        [HttpGet]
        public ActionResult Calcular()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Calcular(Bono bono)
        {
            using (var context = new FinanzasDBEntities())
            {
                bono.UsuarioID = SessionHelper.User.Id;
                SessionHelper.tipoActor = bono.tipoActor;
                SessionHelper.nombreBono = bono.nombre;
                context.Bono.Add(bono);
                context.SaveChanges();
            }
            
            return RedirectToAction("Resultados", bono);
        }

        [HttpGet]
        public ActionResult Resultados(Bono bono)
        {
            Estructuracion estructura = new Estructuracion();
            estructura = Finanzas.Helpers.Finanzas.ResultadosEstructuracion(bono);
            List<Periodo> periodos = new List<Periodo>();
            periodos = Finanzas.Helpers.Finanzas.ResultadosPeriodos(bono, estructura);
            ResultadosViewModel resultados = new ResultadosViewModel();
            resultados.estructura = estructura;
            resultados.periodos = periodos;
            ViewBag.tipoActor = SessionHelper.tipoActor;
            ViewBag.nombre = SessionHelper.nombreBono;
            return View(resultados);
        }


    }
}