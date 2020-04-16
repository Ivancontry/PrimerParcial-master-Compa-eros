using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Credito : Entity<int>, ICredito
    {

        public Credito()
        {
            Pagos = new List<Pago>();
            Cuotas = new List<Cuota>();
        }
        public Credito(double valor, double tasaInteres, int plazoMeses)
        {
            this.Valor = valor;
            this.TasaInteres = tasaInteres;
            this.PlazoMeses = plazoMeses;
            Pagos = new List<Pago>();
            Cuotas = new List<Cuota>();
            CrearCuotas();
        }
        public string CodigoCredito { get; set; }
        public double Valor { get; set; }
        public double ValorAPagar { get { return Valor * (1 + TasaInteres * PlazoMeses); } }
        public double SaldoRestante { get { return ValorAPagar - ObtenerTotalAbonado(); } }
        public int PlazoMeses { get; set; }
        public double TasaInteres { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<Pago> Pagos { get; set; }
        public List<Cuota> Cuotas { get; set; }
        public const int PLAZOMAXIMOENMESES = 10;
        public const double VALORMINIMODELCREDITO = 5000000;
        public const double VALORMAXIMODELCREDITO = 10000000;

        private void CrearCuotas()
        {
            int cont = 1;
            var valorCuota = ValorAPagar / PlazoMeses;
            do
            {
                FechaCreacion = DateTime.Now.Date;
                Cuotas.Add(new Cuota(valorCuota, FechaCreacion.AddMonths(cont)));
                cont++;
            } while (cont <= PlazoMeses);
        }
        
        private double ObtenerTotalAbonado()
        {
            var valorAbonado=0.0;
            Pagos.ForEach(x => { valorAbonado += x.Valor; });
            return valorAbonado;
        }
             
        public string Abonar(double valor)
        {
            var valorAbonar = valor;
            if (CanAbonar(valor).Count != 0) {
                throw new InvalidOperationException();         
            }
            var cuotasPendientesPorPagar = ObtenerCuotasPendientesPorPagar();
            int i = 0;
           
            while (valor>0)
            {
               
                if (cuotasPendientesPorPagar[i].ValorRestantePorPagar() <= valor)
                {
                   valor -= cuotasPendientesPorPagar[i].ValorRestantePorPagar();
                   cuotasPendientesPorPagar[i].ValorPagado += cuotasPendientesPorPagar[i].ValorRestantePorPagar();
                   cuotasPendientesPorPagar[i].Estado = Estado.Pagado;
                }
                else {
                    cuotasPendientesPorPagar[i].ValorPagado += valor;
                    valor -= valor;
                }
               
                
                i++;
            }
           Pagos.Add(new Pago(valorAbonar));
            return "Su cuota se registró correctamente por un valor "+valorAbonar+ ", saldo del crédito restante "+SaldoRestante;
        }

        public IList<string> CanAbonar(double valor)
        {
            var errores = new List<string>();
                       
            if (valor < 0) errores.Add("Error, valor a abonar incorrecto");
            if (valor > SaldoRestante)
            {
                errores.Add("Error, valor a abonar supera al Saldo del Crédito");
            }
            else
            {
                if (valor < ObtenerCuotasPendientesPorPagar().FirstOrDefault().ValorRestantePorPagar())
                {
                    errores.Add("Error, El valor mínimo a pagar es de " + ObtenerCuotasPendientesPorPagar().FirstOrDefault().ValorRestantePorPagar());
                }
            }

            return errores;
        }
        public List<Cuota>  ObtenerCuotasPendientesPorPagar()
        {

           return this.Cuotas.Where(x=> x.Estado == Estado.Pendiente ).OrderBy(x=> x.FechaCreacion).ToList();
        }

        

    }
}
