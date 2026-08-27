using MySql.Data.MySqlClient;
using SolutionDiarioSenac.Classes;

public class Login
{
    private MeuDiarioSENACContext conexao = new MeuDiarioSENACContext();

    public ResultadoCadastro CadastrarUsuario(string nome, string email, string senha)
    {
        try
        {
            

            return ResultadoCadastro.Sucesso;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return ResultadoCadastro.ErroDesconhecido;
        }
    }

    public ResultadoLogin Autenticar(string email, string senha, out Usuario usuarioLogado)
    {
        usuarioLogado = null;

        try
        {
            return ResultadoLogin.Sucesso;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return ResultadoLogin.ErroDesconhecido;
        }
    }
}