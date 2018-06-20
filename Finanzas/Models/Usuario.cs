using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finanzas.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
    }
}