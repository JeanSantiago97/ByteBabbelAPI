using ByteBabbelAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBabbelAPI.Mappings
{
    public class AlunoMap : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(c => c.Cpf).HasMaxLength(15);
            builder.Property(c => c.Nome).HasMaxLength(50);
            builder.Property(c => c.Email).HasMaxLength(50);

            builder
                .HasMany<Matricula>(m => m.Matriculas)
                .WithOne(a => a.Aluno);
        }
    }
}
