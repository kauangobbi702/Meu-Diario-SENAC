using System.Linq;

namespace SolutionDiarioSenac.Classes;
public class RegistroDAL
{
    private MeuDiarioSENACContext conexao = new MeuDiarioSENACContext();

    public void AdicionarRegistro(Registro registro)
    {
        try
        {
            conexao.Registros.Add(registro);
            conexao.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<Registro> ListarRegistros(int idUsuario)
    {
        try
        {
            return conexao.Registros
                .Where(r => r.UsuarioId == idUsuario)
                .OrderByDescending(r => r.Data)
                .ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public List<Registro> BuscarRegistroData(int idUsuario, DateOnly data)
    {
        try
        {
            return conexao.Registros
                .Where(r => r.UsuarioId == idUsuario && r.Data == data)
                .ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public void RemoverRegistro(Registro registro)
    {
        try
        {
            conexao.Registros.Remove(registro);
            conexao.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void EditarRegistro(int idRegistro, string novoTitulo, string novoConteudo)
    {
        try
        {
            Registro registro = conexao.Registros.FirstOrDefault(r => r.Id == idRegistro);

            if (registro != null)
            {
                registro.Titulo = novoTitulo;
                registro.Conteudo = novoConteudo;
                conexao.SaveChanges();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
