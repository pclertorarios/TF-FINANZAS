using Finanzas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finanzas.Helpers
{
    public class SessionHelper
    {
        public static Usuario User { get; set; }
        public static string tipoActor { get; set; }
        public static string nombreBono { get; set; }
        public static int resultadoId { get; set; }
    }
}