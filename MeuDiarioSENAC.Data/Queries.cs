using MySql.Data.MySqlClient;
using SolutionDiarioSenac.Classes;

public class Queries
{
    private MeuDiarioSENACContext conexao = new MeuDiarioSENACContext();

    public void AdicionarRegistro(int idUsuario, string titulo, string conteudo)
    {
        try
        {
            

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            
        }
    }

    public List<Registro> ListarRegistros(int idUsuario)
    {
        try
        {
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
        finally
        {
           
        }
    }

    public List<Registro> BuscarRegistroData(int idUsuario, DateOnly data)
    {
        try
        {
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
        finally
        {
            
        }
    }

}