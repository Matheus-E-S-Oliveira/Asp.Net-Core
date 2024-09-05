using GerenciadorTarefas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorTarefas.EntityConfigs;

public class TarefaEntityConfig : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("tarefa");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Titulo)
            .HasColumnName("titulo")
            .HasColumnType("nvarchar(100)")
            .IsRequired();
            
        builder.Property(x => x.Data)
            .HasColumnName("data")
            .HasColumnType("Date");

        builder.Property(x => x.Completo)
            .HasColumnName("completo")
            .HasColumnType("BOOLEAN");
    }
}