using SolutionDiarioSenac.Classes;
using MeuDiarioSENAC.Business;

Login login = new();
RegistroDAL registroDAO = new RegistroDAL();
UsuarioBusiness usuarioBusiness = new();



while (true)
{
    Console.Clear();
    Console.WriteLine("Bem vindo ao Diário SENAC! O que gostaria de fazer?");
    Console.WriteLine("++++++++++++++++++++++++++++++++++++");
    Console.WriteLine("1 - Entrar na minha conta");
    Console.WriteLine("2 - Cadastrar uma nova conta");
    Console.WriteLine("3 - sair");
    Console.WriteLine("++++++++++++++++++++++++++++++++++++");

    switch (Console.ReadLine())
    {
        case "1":
            Console.WriteLine("Digite o seu e-mail:");
            string emailUsuario = Console.ReadLine() ?? "";
            while (!usuarioBusiness.EmailFoiInformado(emailUsuario))
            {
                Console.WriteLine("\nDigite o seu e-mail:");
                emailUsuario = Console.ReadLine() ?? "";
            }
            

            Console.WriteLine("Digite a sua senha:");
            string senhaUsuario = Console.ReadLine() ?? "";
            while (!usuarioBusiness.SenhaFoiInformada(senhaUsuario))
            {
                Console.WriteLine("\nDigite a sua senha:");
                senhaUsuario = Console.ReadLine() ?? "";
            }

            ResultadoLogin resultadoL = login.Autenticar(emailUsuario, senhaUsuario, out Usuario usuarioLogado);

            switch (resultadoL)
            {
                case ResultadoLogin.Sucesso:
                    
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"Bem vindo ao Diário SENAC {usuarioLogado.Nome}! O que gostaria de fazer?");
                        Console.WriteLine("++++++++++++++++++++++++++++++++++++");
                        Console.WriteLine("1 - Criar novo registro");
                        Console.WriteLine("2 - Listar todos os registros");
                        Console.WriteLine("3 - Buscar registro por data");
                        Console.WriteLine("4 - Sair");
                        Console.WriteLine("++++++++++++++++++++++++++++++++++++");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                Console.WriteLine("Digite o título do registro:");

                                string titulo = Console.ReadLine() ?? "";
                                if (string.IsNullOrWhiteSpace(titulo))
                                {
                                    Console.WriteLine("\nO título não pode estar vazio.");
                                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                                    Console.ReadKey();
                                    break;
                                }

                                Console.WriteLine("\nDigite o que você gostaria de registrar:");

                                string conteudo = Console.ReadLine() ?? "";
                                if (string.IsNullOrWhiteSpace(conteudo))
                                {
                                    Console.WriteLine("\nO conteúdo não pode estar vazio.");
                                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                                    Console.ReadKey();
                                    break;
                                }

                                Registro novoRegistro = new Registro
                                {
                                    UsuarioId = usuarioLogado.Id,
                                    Titulo = titulo,
                                    Conteudo = conteudo,
                                    Data = DateOnly.FromDateTime(DateTime.Now)
                                };

                                registroDAO.AdicionarRegistro(novoRegistro);

                                Console.WriteLine("\nRegistro adicionado com sucesso!");
                                Console.WriteLine("Pressione qualquer tecla para continuar...");
                                Console.ReadKey();
                                break;

                            case "2":

                                try
                                {
                                    List<Registro> registros = registroDAO.ListarRegistros(usuarioLogado.Id);

                                    if (registros == null || registros.Count == 0)
                                    {
                                        Console.WriteLine("\nVocê ainda não possui registros.");
                                        Console.WriteLine("Pressione qualquer tecla para retornar...");
                                        Console.ReadKey();
                                        break;
                                    }

                                    int indiceListagem = 1;
                                    foreach (var registro in registros)
                                    {
                                        Console.WriteLine("++++++++++++++++++++++++++++++++++++\n");
                                        Console.WriteLine($"{indiceListagem} - Título: {registro.Titulo}");
                                        Console.WriteLine($"Data: {registro.Data.ToString("dd/MM/yyyy")}");
                                        Console.WriteLine($"Registro: {registro.Conteudo}\n");
                                        indiceListagem++;
                                    }

                                    Console.WriteLine("Digite o número do registro que deseja editar, ou pressione Enter para voltar:");
                                    string opcaoEdicaoListagem = Console.ReadLine() ?? "";

                                    if (!string.IsNullOrWhiteSpace(opcaoEdicaoListagem))
                                    {
                                        if (int.TryParse(opcaoEdicaoListagem, out int numeroSelecionadoListagem)
                                            && numeroSelecionadoListagem >= 1
                                            && numeroSelecionadoListagem <= registros.Count)
                                        {
                                            EditarRegistroInterativo(registroDAO, registros[numeroSelecionadoListagem - 1]);
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nNúmero inválido.");
                                            Console.WriteLine("Pressione qualquer tecla para retornar...");
                                            Console.ReadKey();
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Erro ao listar registros: " + e.Message);
                                }

                            break;

                            case "3":
                                Console.WriteLine("\nDigite a data do registro (dd/mm/aaaa)");
                                string dataPesquisa = Console.ReadLine() ?? "";
                                if (string.IsNullOrWhiteSpace(dataPesquisa))
                                {
                                    Console.WriteLine("\nA data não pode estar vazia.");
                                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                                    Console.ReadKey();
                                    break;
                                }
                                

                                try
                                {
                                    DateOnly data = DateOnly.ParseExact(dataPesquisa, "dd/MM/yyyy");

                                    List<Registro> registrosData = registroDAO.BuscarRegistroData(usuarioLogado.Id, data);

                                    if (registrosData != null && registrosData.Count > 0)
                                    {
                                        int indiceData = 1;
                                        foreach (var registro in registrosData)
                                        {
                                            Console.WriteLine("++++++++++++++++++++++++++++++++++++\n");
                                            Console.WriteLine($"{indiceData} - Título: {registro.Titulo}");
                                            Console.WriteLine($"Data: {registro.Data.ToString("dd/MM/yyyy")}");
                                            Console.WriteLine($"Registro: {registro.Conteudo}\n");
                                            indiceData++;
                                        }

                                        Console.WriteLine("Digite o número do registro que deseja editar, ou pressione Enter para voltar:");
                                        string opcaoEdicaoData = Console.ReadLine() ?? "";

                                        if (!string.IsNullOrWhiteSpace(opcaoEdicaoData))
                                        {
                                            if (int.TryParse(opcaoEdicaoData, out int numeroSelecionadoData)
                                                && numeroSelecionadoData >= 1
                                                && numeroSelecionadoData <= registrosData.Count)
                                            {
                                                EditarRegistroInterativo(registroDAO, registrosData[numeroSelecionadoData - 1]);
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nNúmero inválido.");
                                                Console.WriteLine("Pressione qualquer tecla para retornar...");
                                                Console.ReadKey();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nNenhum registro encontrado para a data informada.");
                                        Console.WriteLine("Pressione qualquer tecla para retornar...");
                                        Console.ReadKey();
                                    }
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("\nFormato de data inválido. Por favor, use o formato dd/mm/aaaa.");
                                    Console.WriteLine("Pressione qualquer tecla para retornar...");
                                    Console.ReadKey();
                                    break;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("\nErro ao processar a data: " + e.Message);
                                    Console.WriteLine("Pressione qualquer tecla para retornar...");
                                    Console.ReadKey();
                                    break;
                                }                               
                            break;

                            case "4":
                            return;

                            default:
                                Console.WriteLine("\nOpção inválida. Por favor, tente novamente.");
                                Console.WriteLine("Pressione qualquer tecla para continuar...");
                                Console.ReadKey();
                            break;
                        }
                    }

                case ResultadoLogin.EmailNaoEncontrado:
                    Console.WriteLine("\nEmail não cadastrado.");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;

                case ResultadoLogin.SenhaIncorreta:
                    Console.WriteLine("\nE-mail ou senha incorretos.");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;

                case ResultadoLogin.ErroDesconhecido:
                    Console.WriteLine("\nOcorreu um erro ao tentar fazer login.");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
            }

        break;

        case "2":
            Console.WriteLine("\nDigite o seu nome:");
            string nomeCadastro = Console.ReadLine() ?? "";
            while (!usuarioBusiness.NomeFoiInformado(nomeCadastro))
            {
                Console.WriteLine("\nDigite o seu nome:");
                nomeCadastro = Console.ReadLine() ?? "";
            }

            Console.WriteLine("\nDigite o seu e-mail:");
            string emailCadastro = Console.ReadLine() ?? "";
            while (!usuarioBusiness.EmailFoiInformado(emailCadastro))
            {
                Console.WriteLine("\nDigite o seu e-mail:");
                emailCadastro = Console.ReadLine() ?? "";
            }

            Console.WriteLine("\nDigite a sua senha:");
            string senhaCadastro = Console.ReadLine() ?? "";
            while (!usuarioBusiness.SenhaFoiInformada(senhaCadastro))
            {
                Console.WriteLine("\nDigite a sua senha:");
                senhaCadastro = Console.ReadLine() ?? "";
            }
            while (!usuarioBusiness.SenhaMuitoCurta(senhaCadastro))
            {
                Console.WriteLine("\nDigite a sua senha:");
                senhaCadastro = Console.ReadLine() ?? "";
            }

            ResultadoCadastro resultadoC = login.CadastrarUsuario(nomeCadastro, emailCadastro, senhaCadastro);

            switch (resultadoC)
            {
                case ResultadoCadastro.Sucesso:
                    Console.WriteLine("\nUsuário cadastrado com sucesso!");
                    Console.WriteLine("Pressione qualquer tecla para retornar...");
                    Console.ReadKey();
                    break;

                case ResultadoCadastro.EmailDuplicado:
                    Console.WriteLine("\nEsse email já está em uso. Tente fazer login ou use outro email.");
                    Console.WriteLine("Pressione qualquer tecla para retornar...");
                    Console.ReadKey();
                    break;

                case ResultadoCadastro.ErroDesconhecido:
                    Console.WriteLine("\nOcorreu um erro inesperado ao cadastrar. Tente novamente mais tarde.");
                    Console.WriteLine("Pressione qualquer tecla para retornar...");
                    Console.ReadKey();
                    break;
            }
        break;

        case "3":
        return;

        default:
            Console.WriteLine("\nOpção inválida. Por favor, tente novamente.");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        break;
    }
}

static void EditarRegistroInterativo(RegistroDAL registroDAL, Registro registro)
{
    Console.WriteLine("\nDigite o novo título (deixe em branco para manter o atual):");
    string novoTitulo = Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(novoTitulo))
    {
        novoTitulo = registro.Titulo;
    }

    Console.WriteLine("\nDigite o novo conteúdo (deixe em branco para manter o atual):");
    string novoConteudo = Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(novoConteudo))
    {
        novoConteudo = registro.Conteudo;
    }

    registroDAL.EditarRegistro(registro.Id, novoTitulo, novoConteudo);

    Console.WriteLine("\nRegistro atualizado com sucesso!");
    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();
}
