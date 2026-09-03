namespace MeuDiarioSENAC.Business;

public class UsuarioBusiness
{
    public bool NomeFoiInformado(string nome)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome do usuário não foi informado.");
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

    public bool EmailFoiInformado(string email)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("O email do usuário não foi informado.");
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

    public bool SenhaFoiInformada(string senha)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new ArgumentException("A senha do usuário não foi informada.");
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

    public bool SenhaMuitoCurta(string senha)
    {
        try
        {
            if (senha.Length < 6)
            {
                throw new ArgumentException("A senha deve ter pelo menos 6 caracteres.");
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
}
