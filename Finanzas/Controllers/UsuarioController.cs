using Finanzas.Helpers;
using Finanzas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Finanzas.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario user)
        {
            if (ModelState.IsValid)
            {
                if (UsuarioValido(user.username, user.password))
                {
                    using (var context = new FinanzasModel())
                    {
                        var currentUser = context.Usuario.FirstOrDefault(x => x.username == user.username);
                        SessionHelper.User = currentUser;
                    }
                    return RedirectToAction("Index", "Bono");
                }
                else
                {
                    ModelState.AddModelError("", "Datos de Login Incorrectos");
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Usuario user)
        {
            if (ModelState.IsValid)
            {
                using (var context = new FinanzasModel())
                {
                    if (context.Usuario.Where(x=>x.username == user.username).Count() != 0)
                    {
                        ModelState.AddModelError("", "Usuario ya existente");
                    }
                    else
                    {
                        var usuario = context.Usuario.Add(user);
                        context.SaveChanges();
                        return RedirectToAction("Login", "Usuario");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Ingrese Datos Correctos por favor");
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }
        private bool UsuarioValido(String username, String password)
        {
            using (var context = new FinanzasModel())
            {
                var respuesta = context.Usuario.Where(x=>x.username == username && x.password == password).Count();
                return respuesta != 0;
            }
        }
    }
}