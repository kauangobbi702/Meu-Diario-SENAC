using MeuDiarioSENAC.Classes;

public class RegistroBusiness
{
    public bool TituloFoiInformado(string titulo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException("O título do registro não foi informado.");
            }

            return true;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
            return false;
        }
    }

    public bool TituloNaoEhMuitoCurto(string titulo)
    {
        try
        {
            if (titulo.Length < 2)
            {
                throw new ArgumentException("O título do registro deve ter pelo menos 2 caracteres.");
            }

            return true;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
            return false;
        }
    }

    public bool TituloNaoEhMuitoLongo(string titulo)
    {
        try
        {
            if (titulo.Length > 150)
            {
                throw new ArgumentException("O título do registro não pode ter mais de 150 caracteres.");
            }
            return true;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
            return false;
        }
    }


    public bool ConteudoFoiInformado(string conteudo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(conteudo))
            {
                throw new ArgumentException("O conteúdo do registro não foi informado.");
            }
            return true;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
            return false;
        }
    }

    public bool ConteudoNaoEhMuitoCurto(string conteudo)
    {
        try
        {
            if (conteudo.Length < 2)
            {
                throw new ArgumentException("O conteúdo do registro deve ter pelo menos 2 caracteres.");
            }
            return true;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
            return false;
        }
    }
    
    public bool ConteudoNaoEhMuitoLongo(string conteudo)
    {
        try
        {
            if (conteudo.Length > 3000)
            {
                throw new ArgumentException("O conteúdo do registro não pode ter mais de 3000 caracteres.");
            }
            return true;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
            return false;
        }
    }

    public bool ListaRegistroVazia(List<Registro> registros)
    {
        try
        {
            if (registros == null || registros.Count == 0)
            {
                Console.WriteLine("\nVocê ainda não possui registros.");
                Console.WriteLine("Pressione qualquer tecla para retornar...");
                Console.ReadKey();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao verificar lista de registros: " + ex.Message);
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            return false;
        }
    }
}