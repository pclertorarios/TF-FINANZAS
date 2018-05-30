using Finanzas.Models.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finanzas.ViewModels
{
    public class ResultadosViewModel
    {
        public Estructuracion estructura { get; set; }
        public List<Periodo> periodos { get; set; }
        public Utilidad utilidad { get; set; }
    }
}