using ByteBabbelAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBabbelAPI.Mappings
{
    public class TurmaMap : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(t => t.ID);
            builder.Property(t => t.Numero);
            builder.Property(t => t.Anoletivo);

            builder
                .HasMany<Matricula>(m => m.Matriculas)
                .WithOne(a => a.Turma);
        }
    }
}