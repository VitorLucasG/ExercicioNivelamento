using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria {
       
        public ContaBancaria(int numero,string titular,double depositoInicial = 0)
        {
            this.Numero = numero;
            this.Titular = titular;
            this.Saldo += depositoInicial;
        }

        private int Numero { get; set; }

        private string Titular { get; set; }

        private double Saldo { get; set; }

        public void Deposito(double quantia)
        {
            Saldo += quantia;
        }
       
        public void Saque(double quantia)
        {
            Saldo -= quantia;
        }

        public string RetornarDetalhes()
        {
            var result = $"Conta {this.Numero}, Titular: {this.Titular}, Saldo: $ {this.Saldo.ToString("N2")}";

            return result;
        }

    }
}
