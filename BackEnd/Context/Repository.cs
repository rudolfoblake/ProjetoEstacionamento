using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.Context
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;
    
        public Repository(DataContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T : class 
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Preco[]> GetAllPrecosAsync()
        {
            IQueryable<Preco> query = _context.Preco;

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }
        
        public async Task<Preco> GetPrecoAsyncById(int precoId)
        {
            IQueryable<Preco> query = _context.Preco;
                                   
            query = query.AsNoTracking()
                         .OrderBy(p => p.Id)
                         .Where(p => p.Id == precoId);

            return await query.FirstOrDefaultAsync();
        }

         
        public async Task<Preco> GetPrecoByVigencia(DateTime data)
        {
            IQueryable<Preco> query = _context.Preco;
                                   
            query = query.AsNoTracking()
                         .OrderBy(p => p.Id)
                         .Where(p => p.VigenciaInicial <= data && p.VigenciaFinal >= data);

            return await query.FirstOrDefaultAsync();
        }        
        
        public async Task<Veiculo[]> GetAllVeiculosAsync(bool includeEstacionamento)
        {
            IQueryable<Veiculo> query = _context.Veiculo;

            if (includeEstacionamento)
            {
                query = query.Include(v => v.Estacionamentos);                                   
            }

            query = query.AsNoTracking()
                         .OrderBy(v => v.Id);
            return await query.ToArrayAsync();
        }
                
        public async Task<Veiculo> GetCarbyId(int veiculoId)
        {
            IQueryable<Veiculo> query = _context.Veiculo;
                                   
            query = query.AsNoTracking()
                         .OrderBy(v => v.Id)
                         .Where(v => v.Id == veiculoId);

            return await query.FirstOrDefaultAsync();

        }

        public async Task<Estacionamento[]> GetAllEstacionamentosAsync(bool includeVeiculo)
        {
            IQueryable<Estacionamento> query = _context.Estacionamento;
            
            if (includeVeiculo)
            {
                query = query.Include(e => e.Veiculo);
            }

            query = query.AsNoTracking()
                         .OrderBy(e => e.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Estacionamento> GetEstacionamentoAsyncById(int estacionamentoId)
        {
            IQueryable<Estacionamento> query = _context.Estacionamento;
            query = query.Include(e => e.Preco);
                                   
            query = query.AsNoTracking()
                         .OrderBy(e => e.Id)
                         .Where(e => e.Id == estacionamentoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Estacionamento[]> GetEstacionamentoAsyncByVeiculoPlaca(string veiculoPlaca, bool includeVeiculo)
        {
            IQueryable<Estacionamento> query = _context.Estacionamento;            

            if (includeVeiculo)
            {
                query = query.Include(e => e.Veiculo);
            }

            query = query.AsNoTracking()
                        .OrderBy(e => e.Veiculo.Placa)
                        .Where(e => e.Veiculo.Placa == veiculoPlaca);
            return await query.ToArrayAsync();
        }
    }
}


