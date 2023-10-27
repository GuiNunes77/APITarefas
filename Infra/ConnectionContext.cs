using Microsoft.EntityFrameworkCore;
using Tarefas.Models;

namespace Tarefas.Infra
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Host=localhost;Port=5432;Database=Tarefas;Username=postgres;Password=admin";

                optionsBuilder.UseNpgsql(connectionString);

            }
        }
    }
}
