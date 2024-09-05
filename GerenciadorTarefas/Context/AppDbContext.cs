using GerenciadorTarefas.EntityConfigs;
using GerenciadorTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Context;

public class AppDbContext : DbContext
{
    public DbSet<Tarefa> Tarefas => Set<Tarefa>();

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseMySql(
            "server=localhost;port=3306;userid=root;password=1515;database=TreinaWeb",
            new MySqlServerVersion(new Version(8, 0, 39))
        );
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TarefaEntityConfig());
    }
}