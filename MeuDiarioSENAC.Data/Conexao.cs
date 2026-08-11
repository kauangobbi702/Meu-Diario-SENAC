using MySql.Data.MySqlClient;

class Conexao
{
    string StringConexao = "Server=localhost;Port=3306;Database=db_diario_senac;Uid=root;Pwd=1234;";
    public MySqlConnection Conectar()
    {
        MySqlConnection conexao;
        try
        {
            conexao = new MySqlConnection(StringConexao);
            return conexao;
        }   
        catch (Exception e)
        {
            Console.WriteLine("Erro ao criar conexão: " + e.Message);
            return null;
        }
    }
}