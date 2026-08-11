using MySql.Data.MySqlClient;
using SolutionDiarioSenac.Classes;

public class Queries
{
    MySqlConnection conexao = new Conexao().Conectar();

    public void AdicionarRegistro(int idUsuario, string titulo, string conteudo)
    {
        try
        {
            conexao.Open();

            string sql = "INSERT INTO tb_registros (id_usuario, titulo, conteudo) VALUES (@id_usuario, @titulo, @conteudo)";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
            cmd.Parameters.AddWithValue("@titulo", titulo);
            cmd.Parameters.AddWithValue("@conteudo", conteudo);
            cmd.ExecuteNonQuery();

            conexao.Close();
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
            conexao.Open();

            string sql = "SELECT * FROM tb_registros WHERE id_usuario = @id_usuario ORDER BY data DESC";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
            MySqlDataReader reader = cmd.ExecuteReader();

            List <Registro> registros = new();


            while (reader.Read())
            {
                Registro registro = new Registro()
                {
                    Titulo = Convert.ToString(reader["titulo"]),
                    Data = DateOnly.FromDateTime(reader.GetDateTime("data")),
                    Conteudo = Convert.ToString(reader["conteudo"])
                };
                registros.Add(registro);
            }

            if (registros.Count == 0)
            {
                Console.WriteLine("Nenhum registro foi encontrado para a data informada");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return null;
            }

            return registros;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
        finally
        {
            conexao.Close();
        }
    }

    public List<Registro> BuscarRegistroData(int idUsuario, DateOnly data)
    {
        try
        {
            conexao.Open();

            string sql = "SELECT * FROM tb_registros WHERE id_usuario = @id_usuario AND data = @data";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
            cmd.Parameters.AddWithValue("@data", data.ToString("yyyy-MM-dd"));

            MySqlDataReader reader = cmd.ExecuteReader();

            List <Registro> registros = new();


            while (reader.Read())
            {
                Registro registro = new Registro()
                {
                    Titulo = Convert.ToString(reader["titulo"]),
                    Data = DateOnly.FromDateTime(reader.GetDateTime("data")),
                    Conteudo = Convert.ToString(reader["conteudo"])
                };
                registros.Add(registro);
            }
            

            if (registros.Count == 0)
            {
                Console.WriteLine("Nenhum registro foi encontrado para a data informada");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return null;
            }            

            return registros;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
        finally
        {
            conexao.Close();
        }
    }


    /*
    public void AdicionarDAO(Aluno aluno)
    {
        try
        {
            conexao.Open();

            string sql = "INSERT INTO tb_aluno (nome, idade, cpf, estado, cidade, bairro, rua, numero_rua, modalidade, polo) VALUES (@nome, @idade, @cpf, @estado, @cidade, @bairro, @rua, @numero_rua, @modalidade, @polo)";


            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@nome", aluno.Nome);
            comando.Parameters.AddWithValue("@idade", aluno.Idade);
            comando.Parameters.AddWithValue("@cpf", aluno.CPF);
            comando.Parameters.AddWithValue("@estado", aluno.Estado);
            comando.Parameters.AddWithValue("@Cidade", aluno.Cidade);
            comando.Parameters.AddWithValue("@Bairro", aluno.Bairro);
            comando.Parameters.AddWithValue("@Rua", aluno.Rua);
            comando.Parameters.AddWithValue("@numero_rua", aluno.NumeroCasa);
            comando.Parameters.AddWithValue("@modalidade", "Presencial");
            comando.Parameters.AddWithValue("@polo", null);

            comando.ExecuteNonQuery();

            Console.WriteLine("Alunos cadastrados com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<Aluno> ListarTodosDAO()
    {
        try
        {
            conexao.Open();
            string sql = "SELECT * FROM tb_aluno;";

            MySqlCommand comando = new MySqlCommand(sql, conexao);
            MySqlDataReader leitor = comando.ExecuteReader();

            List<Aluno> alunos = new();

            while (leitor.Read())
            {
                Aluno aluno = new Aluno()
                {
                    Id = Convert.ToInt32(leitor["id_aluno"]),
                    Nome = Convert.ToString(leitor["nome"]),
                    Idade = Convert.ToInt32(leitor["idade"]),
                    CPF = Convert.ToString(leitor["cpf"]),
                    Estado = Convert.ToString(leitor["estado"]),
                    Cidade = Convert.ToString(leitor["cidade"]),
                    Bairro = Convert.ToString(leitor["bairro"]),
                    Rua = Convert.ToString(leitor["rua"]),
                    NumeroCasa = Convert.ToInt32(leitor["numero_rua"]),
                    Modalidade = Convert.ToString(leitor["modalidade"]),
                    Polo = Convert.ToString(leitor["polo"])
                };
                alunos.Add(aluno);
            }

            return alunos;

        }
        catch (Exception e)
        {
            return null;
            Console.WriteLine(e.Message);
        }
    }

    public void AtualizarDAO(Aluno aluno)
    {
        try
        {
            conexao.Open();
            string sql = "UPDATE tb_aluno SET idade = @idade WHERE id_aluno = @id;";

            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@Idade", aluno.Idade);
            comando.Parameters.AddWithValue("@id", aluno.Id);

            comando.ExecuteNonQuery();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void ExcluirDAO(Aluno aluno)
    {
        try
        {
            conexao.Open();
            string sql = "DELETE FROM tb_aluno WHERE id_aluno = @id;";

            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@id", aluno.Id);

            comando.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    */

}