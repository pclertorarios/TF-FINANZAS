using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finanzas.Models.Resultados
{
    public class Estructuracion
    {
        public int totalPeriodos { get; set; }
        public double TEA { get; set; }
        public double TEP { get; set; }
        public double COK { get; set; }
        public double costesIniciales { get; set; }
    }
}