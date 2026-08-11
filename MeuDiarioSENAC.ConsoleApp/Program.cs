using SolutionDiarioSenac.Classes;

Login login = new();
Queries queries = new Queries();



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
            if (string.IsNullOrWhiteSpace(emailUsuario))
            {
                Console.WriteLine("\nO e-mail não pode estar vazio.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                break;
            }

            Console.WriteLine("Digite a sua senha:");
            string senhaUsuario = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(senhaUsuario))
            {
                Console.WriteLine("\nA senha não pode estar vazio.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                break;
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

                                Console.WriteLine("Digite o que você gostaria de registrar:");

                                string conteudo = Console.ReadLine() ?? "";
                                if (string.IsNullOrWhiteSpace(conteudo))
                                {
                                    Console.WriteLine("\nO conteúdo não pode estar vazio.");
                                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                                    Console.ReadKey();
                                    break;
                                }

                                queries.AdicionarRegistro(usuarioLogado.ID, titulo, conteudo);

                                Console.WriteLine("\nRegistro adicionado com sucesso!");
                                Console.WriteLine("Pressione qualquer tecla para continuar...");
                                Console.ReadKey();
                                break;

                                case "2":
                                
                                try
                                {

                                    foreach (var registro in queries.ListarRegistros(usuarioLogado.ID))
                                    {
                                        Console.WriteLine("++++++++++++++++++++++++++++++++++++\n");;
                                        Console.WriteLine($"Título: {registro.Titulo}");
                                        Console.WriteLine($"Data: {registro.Data.ToString("dd/MM/yyyy")}");
                                        Console.WriteLine($"Registro: {registro.Conteudo}\n");
                                    } 

                                    Console.WriteLine("Pressione qualquer tecla para retornar...");
                                    Console.ReadKey();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Erro ao listar registros: " + e.Message);
                                }
                                
                            break;

                            case "3":
                                Console.WriteLine("Digite a data do registro (dd/mm/aaaa)");
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

                                    List<Registro> registrosData = queries.BuscarRegistroData(usuarioLogado.ID, data);

                                    if (registrosData != null)
                                    {
                                        foreach (var registro in registrosData)
                                        {
                                            Console.WriteLine("++++++++++++++++++++++++++++++++++++\n");
                                            Console.WriteLine($"Título: {registro.Titulo}");
                                            Console.WriteLine($"Data: {registro.Data.ToString("dd/MM/yyyy")}");
                                            Console.WriteLine($"Registro: {registro.Conteudo}\n");
                                        }

                                        Console.WriteLine("Pressione qualquer tecla para retornar...");
                                        Console.ReadKey();
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
                    Console.WriteLine("\nSenha incorreta.");
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
            Console.WriteLine("Digite o seu nome:");
            string nomeCadastro = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(nomeCadastro))
            {
                Console.WriteLine("\nO nome não pode estar vazio.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                break;
            }

            Console.WriteLine("Digite o seu e-mail:");
            string emailCadastro = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(emailCadastro))
            {
                Console.WriteLine("O e-mail não pode estar vazio.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                break;
            }

            Console.WriteLine("Digite a sua senha:");
            string senhaCadastro = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(senhaCadastro))
            {
                Console.WriteLine("\nA senha não pode estar vazia.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                break;
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
