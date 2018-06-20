using Finanzas.Models.Resultados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finanzas.Models
{
    public class Resultado
    {
        [Key]
        public int Id { get; set; }
        public Estructuracion estructura { get; set; }
        public List<Periodo> periodos { get; set; }
        public Utilidad utilidad { get; set; }
        public RatiosDesicion ratios { get; set;}
    }
}