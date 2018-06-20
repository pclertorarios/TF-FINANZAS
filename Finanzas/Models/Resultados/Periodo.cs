using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finanzas.Models.Resultados
{
    public class Periodo
    {
        [Key]
        public int Id { get; set; }
        public int N { get; set; }
        public string plazoGracia { get; set; }
        public double? bono { get; set; }
        public double? cupon { get; set; }
        public double? cuota { get; set; }
        public double? amortizacion { get; set; }
        public double? prima { get; set; }
        public double? escudo { get; set; }
        public double flujo { get; set; }
        public double? flujoEscudo { get; set; }
        public double? flujoActivo { get; set; }
        public double? flujoActivoPlazo { get; set; }
        public double? factorConvexidad { get; set; }
    }
}