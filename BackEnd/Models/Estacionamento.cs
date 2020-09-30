using System;

namespace BackEnd.Models
{
    public class Estacionamento
    {
        public Estacionamento() {}

        public Estacionamento(int id, DateTime dataEntrada, DateTime dataSaida, double valorTotal, int veiculoId, int precoId)
        {
            this.Id = id;
            this.DataEntrada = dataEntrada;
            this.DataSaida = dataSaida;
            this.ValorTotal = valorTotal;   
            this.VeiculoId = veiculoId;
            this.PrecoId = precoId;         
        }

        public int Id { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public double ValorTotal { get; set; }
        public int VeiculoId { get; set; }
        public int PrecoId { get; set; }
        public Veiculo Veiculo { get; set; }        
        public Preco Preco { get; set; }
    }
}




