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

    public bool TituloNaoEhMuitoCurto(string titulo) => _registroBusiness.TituloNaoEhMuitoCurto(titulo);

    public bool TituloNaoEhMuitoLongo(string titulo) => _registroBusiness.TituloNaoEhMuitoLongo(titulo);

    public bool ConteudoFoiInformado(string conteudo) => _registroBusiness.ConteudoFoiInformado(conteudo);

    public bool ConteudoNaoEhMuitoCurto(string conteudo) => _registroBusiness.ConteudoNaoEhMuitoCurto(conteudo);

    public bool ConteudoNaoEhMuitoLongo(string conteudo) => _registroBusiness.ConteudoNaoEhMuitoLongo(conteudo);

    public void AdicionarRegistro(Registro registro) => _registroContext.AdicionarRegistro(registro);

    public Registro CriarRegistro(string titulo, string conteudo, int usuarioId)
    {
        try
        {
            if (_registroBusiness.TituloFoiInformado(titulo) &&
                _registroBusiness.TituloNaoEhMuitoCurto(titulo) &&
                _registroBusiness.TituloNaoEhMuitoLongo(titulo) &&
                _registroBusiness.ConteudoFoiInformado(conteudo) &&
                _registroBusiness.ConteudoNaoEhMuitoCurto(conteudo) &&
                _registroBusiness.ConteudoNaoEhMuitoLongo(conteudo))
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

    public void EditarRegistro(int idRegistro, string novoTitulo, string novoConteudo) => _registroContext.EditarRegistro(idRegistro, novoTitulo, novoConteudo);

    public bool ListaRegistroVazia(List<Registro> registros) => _registroBusiness.ListaRegistroVazia(registros);
}