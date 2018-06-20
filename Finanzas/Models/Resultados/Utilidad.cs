using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finanzas.Models.Resultados
{
    public class Utilidad
    {
        [Key]
        public int Id { get; set; }
        public double precioActual { get; set; }
        public double utilidad { get; set; }
    }
}