using Finanzas.Helpers;
using Finanzas.Models;
using Finanzas.Models.Resultados;
using Finanzas.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            int ID;
            using (var context = new FinanzasDBEntities())
            {
                bono.UsuarioID = SessionHelper.User.Id;
                context.Bono.Add(bono);
                context.SaveChanges();
                ID = bono.Id;
            }
            return RedirectToAction("Resultados", new { bonoId = ID });
        }

        [HttpGet]
        public ActionResult Resultados(int? bonoId)
        {
            Bono bono;
            using (var context = new FinanzasDBEntities())
            {
                bono = context.Bono.FirstOrDefault(x => x.Id == bonoId);
                SessionHelper.tipoActor = bono.tipoActor;
                SessionHelper.nombreBono = bono.nombre;
            }
            Estructuracion estructura = new Estructuracion();
            estructura = Finanzas.Helpers.Finanzas.ResultadosEstructuracion(bono);
            List<Periodo> periodos = new List<Periodo>();
            periodos = Finanzas.Helpers.Finanzas.ResultadosPeriodos(bono, estructura);
            Utilidad utilidad = new Utilidad();
            utilidad = Finanzas.Helpers.Finanzas.ResultadosUtilidad(periodos, estructura, bono);
            RatiosDesicion ratios = new RatiosDesicion();
            ratios = Finanzas.Helpers.Finanzas.ResultadosRatios(periodos, estructura, bono);
            ResultadosViewModel resultados = new ResultadosViewModel();
            resultados.estructura = estructura;
            resultados.periodos = periodos;
            resultados.utilidad = utilidad;
            resultados.ratios = ratios;
            ViewBag.tipoActor = SessionHelper.tipoActor;
            ViewBag.nombre = SessionHelper.nombreBono;
            return View(resultados);
        }

        [HttpGet]
        public ActionResult Listar()
        {
            ListaBonosViewModel vm = new ListaBonosViewModel();
            using (var context = new FinanzasDBEntities())
            {
                vm.Bonos = context.Bono.Where(x => x.UsuarioID == SessionHelper.User.Id).ToList();
            }
            return View(vm.Bonos);
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            using (var context = new FinanzasDBEntities())
            {
                var bono = context.Bono.FirstOrDefault(x => x.Id == id);
                context.Bono.Remove(bono);
                context.SaveChanges();
            }
            return RedirectToAction("Listar");
        }

    }
}