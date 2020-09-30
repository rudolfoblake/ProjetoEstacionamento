using System;
using BackEnd.Models;

namespace BackEnd.Services
{
    public class CalculoEstacionamentoService : ICalculoEstacionamento
    {
        public double CalcularValor(Estacionamento estacionamento) 
        {
            var valorDaHora = estacionamento.Preco.ValorHora;

            TimeSpan totalMinutosHoras = DateTime.Now.Subtract(estacionamento.DataEntrada);

            double valorMinuto = 0;

            if ((int)totalMinutosHoras.Minutes > 10)
            {
               valorMinuto = valorDaHora / 2;
            }
          
            return ((int)totalMinutosHoras.TotalHours * valorDaHora) + valorMinuto;
            
        }
        
    }
}


            

    