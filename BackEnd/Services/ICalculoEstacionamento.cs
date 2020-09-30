
using System.Threading.Tasks;
using BackEnd.Models;
using System;

namespace BackEnd.Services
{
    public interface ICalculoEstacionamento
    {
        double CalcularValor(Estacionamento estacionamento);        
    }
}



 