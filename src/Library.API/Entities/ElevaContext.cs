using Microsoft.EntityFrameworkCore;

namespace ElevaCase.API.Entities
{
    public class ElevaContext : DbContext
    {
        public ElevaContext(DbContextOptions<ElevaContext> options)
           : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Turma> Turmas { get; set; }
    }
}
