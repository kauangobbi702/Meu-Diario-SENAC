using System.Linq;
using SolutionDiarioSenac.Classes;

public class Login
{
    private MeuDiarioSENACContext conexao = new MeuDiarioSENACContext();

    public ResultadoCadastro CadastrarUsuario(string nome, string email, string senha)
    {
        try
        {
            bool emailJaExiste = conexao.Usuarios.Any(u => u.Email == email);

            if (emailJaExiste)
            {
                return ResultadoCadastro.EmailDuplicado;
            }

            Usuario novoUsuario = new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = senha
            };

            conexao.Usuarios.Add(novoUsuario);
            conexao.SaveChanges();

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
            Usuario usuario = conexao.Usuarios.FirstOrDefault(u => u.Email == email);

            if (usuario == null)
            {
                return ResultadoLogin.EmailNaoEncontrado;
            }

            if (usuario.Senha != senha)
            {
                return ResultadoLogin.SenhaIncorreta;
            }

            usuarioLogado = usuario;
            return ResultadoLogin.Sucesso;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return ResultadoLogin.ErroDesconhecido;
        }
    }
}
