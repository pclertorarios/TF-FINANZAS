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
            return Math.Round(Math.Pow(1 + (TNP / m), m) - 1, 9);
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
            return Math.Round(Math.Pow(1 + TEA, fraccion) - 1, 9);
        }

        public static double HallarCOK(double tasaDescuento, int diasAño, int frecuencia)
        {
            double fraccion = (double)frecuencia / diasAño;
            return Math.Round(Math.Pow(1 + tasaDescuento, fraccion) - 1, 9);
        }

        public static double HallarCostesIniciales(string tipoActor, double valorComercial, double pEstructuracion, double pColocacion, double pFlotacion, double pCAVALI)
        {
            double suma = tipoActor == "Emisor" ? pEstructuracion + pFlotacion + pColocacion + pCAVALI : pFlotacion + pCAVALI;
            return Math.Round(suma * valorComercial, 2);
        }

        public static double HallarCuota(double bono, double TEP, int numeroCuotas)
        {
            return -Math.Round(bono * (Math.Pow(1 + TEP, numeroCuotas) * TEP) / (Math.Pow(1 + TEP, numeroCuotas) - 1), 2);
        }

        public static double HallarPrecioActual(List<Periodo> periodos, Estructuracion estructura)
        {
            double resultado = 0;
            for (int i = 1; i < periodos.Count; i++)
            {
                resultado = resultado + (periodos[i].flujo / Math.Pow(estructura.COK + 1, i));
            }
            return Math.Round(resultado,2);
        }

        public static RatiosDesicion ResultadosRatios(List<Periodo> periodos, Estructuracion estructura, Bono bono)
        {
            double sumaFAP = 0;
            double sumaFA = 0;
            double sumaFC = 0;
            for (int i = 1; i < periodos.Count; i++)
            {
                sumaFAP = sumaFAP + periodos[i].flujoActivoPlazo.Value;
                sumaFA = sumaFA + periodos[i].flujoActivo.Value;
                sumaFC = sumaFC + periodos[i].factorConvexidad.Value;
            }
            RatiosDesicion resultado = new RatiosDesicion();
            resultado.duracion = Math.Round(sumaFAP / sumaFA,2);
            resultado.convexidad = Math.Round(sumaFC / (Math.Pow(1 + estructura.COK, 2) * sumaFA * Math.Pow(bono.diasAño / bono.frecuencia, 2)),2);
            resultado.total = Math.Round(resultado.duracion + resultado.convexidad,2);
            resultado.duracionModificada = Math.Round(resultado.duracion / (1 + estructura.COK),2);
            return resultado;
        }

        public static Utilidad ResultadosUtilidad(List<Periodo> periodos, Estructuracion estructura, Bono bono)
        {
            if (bono.tipoActor == "Bonista")
            {
                return new Utilidad
                {
                    precioActual = HallarPrecioActual(periodos, estructura),
                    utilidad = Math.Round(periodos[0].flujo + HallarPrecioActual(periodos, estructura), 2)
                };
            }
            return null;
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
        
        public static List<Periodo> ResultadosPeriodos(Bono bono, Estructuracion estructura, List<Periodo> periodos)
        {
            List<Periodo> lista = new List<Periodo>();
            double flujo = bono.tipoActor == "Emisor" ? Math.Round(bono.vcomercial - estructura.costesIniciales, 7): Math.Round(-bono.vcomercial - estructura.costesIniciales, 7);
            Periodo cero = new Periodo
            {
                N = 0,
                plazoGracia = null,
                bono = null,
                cupon = null,
                cuota = null,
                amortizacion = null,
                prima = null,
                escudo = null,
                flujo = flujo,
                flujoEscudo = bono.tipoActor == "Emisor" ? flujo : (double?) null,
                flujoActivo = null,
                flujoActivoPlazo = null,
                factorConvexidad = null,
            };
            lista.Add(cero);
            for (int i = 1; i <= estructura.totalPeriodos; i++)
            {
                Periodo aux = new Periodo();
                aux.N = i;
                aux.plazoGracia = periodos[i].plazoGracia;
                aux.bono = i == 1 ? bono.vnominal : Math.Round(lista[i-1].bono.Value + lista[i-1].amortizacion.Value,2);
                if (periodos.Count >0 && periodos[i-1].plazoGracia == "T" && i!=1) aux.bono = Math.Round(lista[i-1].bono.Value - lista[i-1].cupon.Value, 2);
                aux.cupon = Math.Round(-aux.bono.Value * estructura.TEP,2);
                aux.cuota = HallarCuota(aux.bono.Value, estructura.TEP, estructura.totalPeriodos - aux.N + 1);
                if (periodos.Count > 0 && periodos[i].plazoGracia == "T") aux.cuota = 0;
                if (periodos.Count > 0 && periodos[i].plazoGracia == "P") aux.cuota = aux.cupon;
                aux.amortizacion = Math.Round(aux.cuota.Value - aux.cupon.Value,2);
                if (periodos.Count > 0 && (periodos[i].plazoGracia == "T" || periodos[i].plazoGracia == "P")) aux.amortizacion = 0;
                aux.prima = aux.N == estructura.totalPeriodos ? -Math.Round(bono.pPrima * bono.vnominal,2) : 0;
                aux.escudo = Math.Round(-aux.cupon.Value * bono.impuestoRenta,2);
                aux.flujo = bono.tipoActor == "Bonista" ? -Math.Round(aux.cuota.Value + aux.prima.Value,2) : Math.Round(aux.cuota.Value + aux.prima.Value, 2);
                aux.flujoEscudo = aux.escudo + aux.flujoEscudo;
                if (bono.tipoActor == "Bonista")
                {
                    aux.flujoActivo = Math.Round(aux.flujo /Math.Pow(1+estructura.COK,aux.N),2);
                    aux.flujoActivoPlazo = Math.Round(aux.flujoActivo.Value * aux.N * bono.frecuencia / bono.diasAño, 2);
                    aux.factorConvexidad = Math.Round(aux.flujoActivo.Value * aux.N * (1 + aux.N), 2);
                }
                lista.Add(aux);
            }
            return lista;
        }
    }
}