﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finanzas.Models.Resultados
{
    public class Rentabilidad
    {
        [Key]
        public int Id { get; set; }
        public double? TCEA { get; set; }
        public double? TREA { get; set; }
    }
}