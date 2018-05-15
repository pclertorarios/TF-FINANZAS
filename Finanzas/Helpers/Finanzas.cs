using Finanzas.Models;
using Finanzas.Models.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finanzas.Helpers
{
    public class Finanzas
    {
        public static double HallarTEA(double TNP, int diasAño, int capitalizacion)
        {
            double m = diasAño / capitalizacion;
            return Math.Round(Math.Pow(1 + (TNP / m), m) - 1, 7);
        }
        
        public static double HallarTEP(double Tasa, int diasAño, int frecuencia, int? capitalizacion)
        {
            double TEA;
            if (capitalizacion.HasValue)
            {
                TEA = HallarTEA(Tasa, diasAño, capitalizacion.Value);
            }
            else
            {
                TEA = Tasa;
            }
            double fraccion = (double)frecuencia / diasAño;
            return Math.Round(Math.Pow(1 + TEA, fraccion ) - 1, 7);
        }

        public static double HallarCOK(double tasaDescuento, int diasAño, int frecuencia)
        {
            double fraccion = (double)frecuencia / diasAño;
            return Math.Round(Math.Pow(1 + tasaDescuento, fraccion) - 1, 7);
        }

        public static double HallarCostesIniciales(string tipoActor, double valorComercial, double pEstructuracion, double pColocacion,double pFlotacion, double pCAVALI)
        {
            double suma = tipoActor == "Bonita" ? pEstructuracion + pFlotacion + pColocacion + pCAVALI : pFlotacion + pCAVALI;
            return Math.Round(suma * valorComercial, 2);
        }

        public static Estructuracion ResultadosEstructuracion(Bono bono)
        {
            return new Estructuracion {
                totalPeriodos = (bono.diasAño / bono.frecuencia) * bono.años,
                TEA = bono.tipoInteres == "Efectiva" ? bono.tasaInteres : HallarTEA(bono.tasaInteres, bono.diasAño, bono.capitalizacion.Value),
                TEP = HallarTEP(bono.tasaInteres, bono.diasAño, bono.frecuencia, bono.capitalizacion),
                COK = HallarCOK(bono.tasaDescuento,bono.diasAño,bono.frecuencia),
                costesIniciales = HallarCostesIniciales(bono.tipoActor,bono.vcomercial, bono.pEstructura,bono.pColoca,bono.pFlota,bono.pCAVALI)  
            };
        }
    }
}