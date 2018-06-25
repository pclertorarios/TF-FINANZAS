using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finanzas.Models.Resultados
{
    public class Estructuracion
    {
        [Key]
        public int Id { get; set; }
        public int totalPeriodos { get; set; }
        public double TEA { get; set; }
        [DisplayFormat(DataFormatString = @"{0:#\%}")]
        public double TEP { get; set; }
        public double COK { get; set; }
        public double costesIniciales { get; set; }
    }
}