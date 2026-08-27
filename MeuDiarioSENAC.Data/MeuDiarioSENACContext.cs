using Microsoft.EntityFrameworkCore;
using SolutionDiarioSenac.Classes;

public class MeuDiarioSENACContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Registro> Registros { get; set; }

    private readonly string StringConexao = "Server=localhost;Port=3306;Database=db_diario_senac;Uid=root;Pwd=1234;";
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(StringConexao, ServerVersion.AutoDetect(StringConexao));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Registros)
            .WithOne(r => r.Usuario)
            .HasForeignKey(r => r.UsuarioId);
    }
}