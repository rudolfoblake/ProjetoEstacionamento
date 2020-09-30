using Microsoft.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Preco> Preco { get; set; }
        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<Estacionamento> Estacionamento { get; set; }
                
    }
}   