using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finanzas.Models
{
    public class Bono
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double vnominal { get; set; }
        [Required]
        public double vcomercial { get; set; }
        [Required]
        public int años { get; set; }
        [Required]
        public int frecuencia { get; set; }
        [Required]
        public int diasAño { get; set; }
        [Required]
        public string tipoInteres { get; set; }
        public int? capitalizacion { get; set; }
        [Required]
        public double tasaInteres { get; set; }
        [Required]
        public double tasaDescuento { get; set; }
        [Required]
        public double impuestoRenta { get; set; }
        [Required]
        public DateTime fechaEmision { get; set; }
        [Required]
        public double pPrima { get; set; }
        [Required]
        public double pEstructura { get; set; }
        [Required]
        public double pColoca { get; set; }
        [Required]
        public double pFlota { get; set; }
        [Required]
        public double pCAVALI { get; set; }
        [Required]
        public int UsuarioID { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string tipoActor { get; set; }

        public Usuario Usuario { get; set; }
        public Resultado Resultado { get; set; }
    }
}