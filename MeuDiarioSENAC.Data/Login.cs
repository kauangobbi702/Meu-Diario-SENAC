using MySql.Data.MySqlClient;
using SolutionDiarioSenac.Classes;

public class Login
{
    public ResultadoCadastro CadastrarUsuario(string nome, string email, string senha)
    {
        try
        {
            using (var conexao = new MeuDiarioSENACContext().Conectar())
            {
                conexao.Open();
                string sql = "INSERT INTO tb_usuario (nome_usuario, email_usuario, senha_usuario) VALUES (@nome, @email, @senha)";

                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    cmd.ExecuteNonQuery();
                }
            }

            return ResultadoCadastro.Sucesso;
        }
        catch (MySqlException e) when (e.Number == 1062)
        {
            Console.WriteLine("Este email já está cadastrado.");
            return ResultadoCadastro.EmailDuplicado;
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
            using (var conexao = new MeuDiarioSENACContext().Conectar())
            {
                conexao.Open();

                string sql = "SELECT * FROM tb_usuario WHERE email_usuario = @email";

                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return ResultadoLogin.EmailNaoEncontrado;
                        }

                        string senhaSalva = Convert.ToString(reader["senha_usuario"]);

                        if (senha != senhaSalva)
                        {
                            return ResultadoLogin.SenhaIncorreta;
                        }

                        usuarioLogado = new Usuario()
                        {
                            ID = Convert.ToInt32(reader["id_usuario"]),
                            Nome = Convert.ToString(reader["nome_usuario"]),
                            Email = Convert.ToString(reader["email_usuario"])
                        };

                        return ResultadoLogin.Sucesso;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return ResultadoLogin.ErroDesconhecido;
        }
    }
}