using Microsoft.EntityFrameworkCore;


public class MeuDiarioSENACContext : DbContext
{
    private readonly string StringConexao = "Server=localhost;Port=3306;Database=db_diario_senac;Uid=root;Pwd=1234;";
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(StringConexao, ServerVersion.AutoDetect(StringConexao));
    }
}