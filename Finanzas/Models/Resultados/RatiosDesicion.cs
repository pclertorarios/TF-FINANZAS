using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finanzas.Models.Resultados
{
    public class RatiosDesicion
    {
        [Key]
        public int Id { get; set; }
        public double duracion { get; set; }
        public double convexidad { get; set; }
        public double total { get; set; }
        public double duracionModificada { get; set;}
    }
}