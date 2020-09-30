
using System.Threading.Tasks;
using BackEnd.Models;
using System;

namespace BackEnd.Context
{
    public interface IRepository
    {
        // Geral

        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        // Preco

        Task<Preco[]> GetAllPrecosAsync(); 
        Task<Preco> GetPrecoAsyncById(int precoId);
        Task<Preco> GetPrecoByVigencia(DateTime data);
        
        // Veiculo

        Task<Veiculo[]> GetAllVeiculosAsync(bool includeEstacionamento); 
        Task<Veiculo> GetCarbyId(int veiculoId);
        
    
        // Estacionamento

        Task<Estacionamento[]> GetAllEstacionamentosAsync(bool includeVeiculo); 
        Task<Estacionamento> GetEstacionamentoAsyncById(int estacionamentoId);
        Task<Estacionamento[]> GetEstacionamentoAsyncByVeiculoPlaca(string veiculoPlaca, bool includeVeiculo);

        
    }
}



 