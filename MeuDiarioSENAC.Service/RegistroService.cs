using MeuDiarioSENAC.Classes;
using MeuDiarioSENAC.Data;

public class RegistroService
{
    private readonly RegistroBusiness _registroBusiness;
    private readonly RegistroContext _registroContext;

    public RegistroService()
    {
        _registroBusiness = new RegistroBusiness();
        _registroContext = new RegistroContext();
    }

    public bool TituloFoiInformado(string titulo) => _registroBusiness.TituloFoiInformado(titulo);

    public bool TituloMuitoCurto(string titulo) => _registroBusiness.TituloMuitoCurto(titulo);

    public bool TituloMuitoLongo(string titulo) => _registroBusiness.TituloMuitoLongo(titulo);

    public bool ConteudoFoiInformado(string conteudo) => _registroBusiness.ConteudoFoiInformado(conteudo);

    public bool ConteudoMuitoCurto(string conteudo) => _registroBusiness.ConteudoMuitoCurto(conteudo);

    public bool ConteudoMuitoLongo(string conteudo) => _registroBusiness.ConteudoMuitoLongo(conteudo);

    public void AdicionarRegistro(Registro registro) => _registroContext.AdicionarRegistro(registro);

    public Registro CriarRegistro(string titulo, string conteudo, int usuarioId)
    {
        try
        {
            if (_registroBusiness.TituloFoiInformado(titulo) &&
                _registroBusiness.TituloMuitoCurto(titulo) &&
                _registroBusiness.TituloMuitoLongo(titulo) &&
                _registroBusiness.ConteudoFoiInformado(conteudo) &&
                _registroBusiness.ConteudoMuitoCurto(conteudo) &&
                _registroBusiness.ConteudoMuitoLongo(conteudo))
            {
                Registro registro = new Registro();
                return registro.CriarRegistro(titulo, conteudo, usuarioId);
            }
            else
            {
                throw new ArgumentException("Não foi possível criar o registro devido a erros de validação.");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
            return null;
        }
    }

    public List<Registro> ListarRegistros(int idUsuario) => _registroContext.ListarRegistros(idUsuario);

    public List<Registro> BuscarRegistroData(int idUsuario, DateOnly data) => _registroContext.BuscarRegistroData(idUsuario, data);

    public void RemoverRegistro(Registro registro) => _registroContext.RemoverRegistro(registro);

    public void EditarRegistroInterativo(RegistroContext registroDAL, Registro registro)
    {
        Console.WriteLine("\nDigite o novo título (deixe em branco para manter o atual):");
        string novoTitulo = Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(novoTitulo))
        {
            novoTitulo = registro.Titulo;
        }
        else if (TituloMuitoCurto(novoTitulo) && TituloMuitoLongo(novoTitulo))
        {
            registro.Titulo = novoTitulo;
        }
 

        Console.WriteLine("\nDigite o novo conteúdo (deixe em branco para manter o atual):");
        string novoConteudo = Console.ReadLine() ?? "";
        if (string.IsNullOrEmpty(novoConteudo))
        {
            novoConteudo = registro.Conteudo;
        }
        else if (ConteudoMuitoCurto(novoConteudo) && ConteudoMuitoLongo(novoConteudo))
        {
            registro.Conteudo = novoConteudo;
        }

        registroDAL.EditarRegistro(registro.Id, novoTitulo, novoConteudo);

        Console.WriteLine("\nRegistro atualizado com sucesso!");
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    public bool ListaRegistroVazia(List<Registro> registros) => _registroBusiness.ListaRegistroVazia(registros);
}