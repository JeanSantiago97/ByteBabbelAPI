using ByteBabbelAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBabbelAPI.Mappings
{
    public class MatriculaMap : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            //Mapeamento para a tabela Matricula N_to_N
            builder.HasKey(m => new { m.AlunoID, m.TurmaID });
        }
    }
}
