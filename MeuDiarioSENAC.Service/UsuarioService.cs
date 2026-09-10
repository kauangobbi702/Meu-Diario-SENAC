namespace MeuDiarioSENAC.Service;

using MeuDiarioSENAC.Business;
using MeuDiarioSENAC.Data;
using MeuDiarioSENAC.Classes;

public class UsuarioService
{

    private readonly Login _login;
    private readonly UsuarioBusiness _usuarioBusiness;

    public UsuarioService()
    {
        _login = new Login();
        _usuarioBusiness = new UsuarioBusiness();
    }

    public ResultadoCadastro CadastrarUsuario(string nome, string email, string senha)
    {
        return _login.CadastrarUsuario(nome, email, senha);
    }

    public ResultadoLogin AutenticarUsuario(string email, string senha, out Usuario usuarioLogado)
    {
        return _login.Autenticar(email, senha, out usuarioLogado);
    }

    public bool NomeFoiInformado(string nome) => _usuarioBusiness.NomeFoiInformado(nome);
    public bool EmailFoiInformado(string email) => _usuarioBusiness.EmailFoiInformado(email);
    public bool SenhaFoiInformada(string senha) => _usuarioBusiness.SenhaFoiInformada(senha);
    public bool SenhaMuitoCurta(string senha) => _usuarioBusiness.SenhaMuitoCurta(senha);
}
