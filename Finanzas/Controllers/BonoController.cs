using Finanzas.Helpers;
using Finanzas.Models;
using Finanzas.Models.Resultados;
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
            Estructuracion resultado = new Estructuracion();
            resultado = Helpers.Finanzas.ResultadosEstructuracion(bono);
            return View(resultado);
        }
    }
}