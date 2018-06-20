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
            using (var context = new FinanzasModel())
            {
                bono.Usuario = context.Usuario.FirstOrDefault(x => x.Id == SessionHelper.User.Id);
                Estructuracion estructura = new Estructuracion();
                estructura = Finanzas.Helpers.Finanzas.ResultadosEstructuracion(bono);
                context.Estructuracion.Add(estructura);
                List<Periodo> periodos = new List<Periodo>();
                periodos = Finanzas.Helpers.Finanzas.ResultadosPeriodos(bono, estructura,periodos);
                foreach (var item in periodos)
                {
                    context.Periodo.Add(item);
                }
                Resultado resultado = new Resultado();
                
                resultado.estructura = estructura;
                resultado.periodos = periodos;
                context.Resultado.Add(resultado);
                bono.Resultado = resultado;
                context.Bono.Add(bono);
                context.SaveChanges();
                ID = resultado.Id; 
            }

            return RedirectToAction("Flujo", new { resultadoId =  ID});
        }
        [HttpGet]
        public ActionResult Flujo(int resultadoId)
        {
            Resultado resultado;
            Bono bono;
            using (var context = new FinanzasModel())
            {
                resultado = context.Resultado.
                    Include(x=>x.estructura).
                    Include(x=>x.periodos).
                    FirstOrDefault(x => x.Id == resultadoId);
                bono = context.Bono.FirstOrDefault(x => x.Resultado.Id == resultadoId);
            }
            SessionHelper.tipoActor = bono.tipoActor;
            SessionHelper.nombreBono = bono.nombre;
            SessionHelper.resultadoId = resultadoId;
            ViewBag.tipoActor = SessionHelper.tipoActor;
            ViewBag.nombre = SessionHelper.nombreBono;
            return View(resultado.periodos);
        }

        [HttpPost]
        public ActionResult Flujo(List<Periodo> periodos)
        {
            Resultado aux;
            Bono bono;
            using (var context = new FinanzasModel())
            {
                aux = context.Resultado.
                    Include(x=>x.periodos).
                    Include(x=>x.estructura).
                    FirstOrDefault(x => x.Id == SessionHelper.resultadoId);
                bono = context.Bono.FirstOrDefault(x => x.Resultado.Id == SessionHelper.resultadoId);
                foreach (var item in aux.periodos)
                {
                    item.plazoGracia = periodos[item.N].plazoGracia;
                }
                aux.periodos = Helpers.Finanzas.ResultadosPeriodos(bono, aux.estructura, aux.periodos);
                context.SaveChanges();
            }

            return RedirectToAction("Flujo", new { resultadoId = SessionHelper.resultadoId });
        }

        [HttpGet]
        public ActionResult Resultados(int? resultadoId)
        {
            Bono bono;
            Resultado resultado;
            using (var context = new FinanzasModel())
            {
                bono = context.Bono.FirstOrDefault(x => x.Resultado.Id == resultadoId);
                resultado = context.Resultado.
                    Include(x=>x.estructura).
                    Include(x=>x.periodos).
                    Include(x=>x.ratios).
                    Include(x=>x.utilidad).
                    FirstOrDefault(x => x.Id == resultadoId);

            }
            
            ViewBag.tipoActor = SessionHelper.tipoActor;
            ViewBag.nombre = SessionHelper.nombreBono;
            return View(resultado);
        }

        [HttpGet]
        public ActionResult Listar()
        {
            ListaBonosViewModel vm = new ListaBonosViewModel();
            using (var context = new FinanzasModel())
            {
                vm.Bonos = context.Bono.
                    Include(x=>x.Resultado).
                    Where(x => x.UsuarioID == SessionHelper.User.Id).ToList();
            }
            return View(vm.Bonos);
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            using (var context = new FinanzasModel())
            {
                var bono = context.Bono.FirstOrDefault(x => x.Id == id);
                context.Bono.Remove(bono);
                context.SaveChanges();
            }
            return RedirectToAction("Listar");
        }

    }
}