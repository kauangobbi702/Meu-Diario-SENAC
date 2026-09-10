namespace MeuDiarioSENAC.Classes;
public class Registro
{
    public int Id { get; set; }
    public string Titulo { get; set;}
    public DateOnly Data { get; set; }
    public string Conteudo { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public Registro CriarRegistro(string titulo, string conteudo, int usuarioId)
    {
        Titulo = titulo;
        Conteudo = conteudo;
        Data = DateOnly.FromDateTime(DateTime.Now);
        UsuarioId = usuarioId;
        return this;
    }    
}