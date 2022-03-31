using ByteBabbelAPI.Mappings;
using ByteBabbelAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ByteBabbelAPI.Data
{
    public class BabbelContext : DbContext
    {//public BabbelContext(DbContextOptions<BabbelContext> opt)


        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoMap());
            modelBuilder.ApplyConfiguration(new MatriculaMap());
            modelBuilder.ApplyConfiguration(new TurmaMap());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=AlphaJ;Initial Catalog=ByteBabbel;Integrated Security=True");


        }
    }
}
