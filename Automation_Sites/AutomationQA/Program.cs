
using AutomationQA.FakeDatas;
using AutomationQA.MenusEnvironments;
using AutomationQA.ReportsTests;
using AutomationQA.Scenarios;
using Microsoft.Extensions.Configuration;

//Environment.SetEnvironmentVariable("PWDEBUG", "1"); // abre o depurador da Playwright e passa linha a linha pelo código!

//Configuração necessária para o banco de dados
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")  // Aqui você lê o arquivo de configurações
    .Build();

var baseConfig = new BaseConfig(configuration);

// Escolhe do ambiente de teste
Environments environments = new Environments();
string selectedEnvironment = environments.AskEnvironment();

// Executa o cenario escolhido
MenuClass menu = new MenuClass();
string selectedMenu = menu.ChooseClass();

// Acessa o navegador
Environments url = new Environments();
var (browser, page) = await url.UrlEnvironment(selectedEnvironment);

if (selectedMenu != null)
{
    var testResults = new List<TestResult>();
    var dados = FakeDataGenerator.GenerateDataFalse("CPF");


// Dicionário para mapear classes e ações
var actions = new Dictionary<string, Func<Task>>

    {
        ["Login"] = async () =>
        {
            await new Login().ValidateLoginCorrect(page, testResults, selectedEnvironment);
            await new Login().LogOff(page);
            await new Login().ValidateLoginIncorrect(page, testResults, selectedEnvironment);
        },
        ["Abrir Chamado"] = async () =>
        {
            await new OpenSupport().ValidateOpenSupport(page, testResults, selectedEnvironment);
            await new OpenSupport().SupportDetails(page, testResults, selectedEnvironment);
        },
        ["Cadastro"] = async () =>
        {
            await new Registration().Register(page, testResults, "CPF", selectedEnvironment);
            await new Registration().Register(page, testResults, "CNPJ", selectedEnvironment);
            await new Registration().Register(page, testResults, "EMAIL", selectedEnvironment);
            await new Registration().UserRegistered(page, testResults, selectedEnvironment);
        },
        ["StatusF"] = async () =>
        {
            await new StatusF(baseConfig).ValidadeStatusF(page, testResults, dados, selectedEnvironment);
        },
        ["Esqueci Senha"] = async () =>
        {
            await new ForgotPassword().RecoverPassword(page, testResults, selectedEnvironment);
        },
        ["Pedido"] = async () =>
        {
            await new Orders(baseConfig).PayWithBankSlip(page, testResults, selectedEnvironment, dados); // pagamento com boleto
            await new Orders(baseConfig).PaymentAndChangeOfAddress(page, testResults, selectedEnvironment, dados); // pagamento trocando endereço
            await new Orders(baseConfig).PayWithPix(page, testResults, selectedEnvironment, dados); //pagmento com pix
            await new Orders(baseConfig).PayWithCreditCard(page, testResults, selectedEnvironment, dados); // pagamento com cartão de crédito
        },
        ["Meus Dados"] = async () =>
        {
            await new MyData().ValidateInvalidDateOfBirth(page, testResults, selectedEnvironment, dados);
            await new MyData().CEPManualFilling(page, testResults, selectedEnvironment, dados);
            await new MyData().ValidateComplement(page, testResults, selectedEnvironment, dados);
            await new MyData().EditAddress(page, testResults, selectedEnvironment, dados);
            await new MyData().DeleteAddress(page, testResults, selectedEnvironment);

        },
        ["Telas"] = async () =>
        {
            await new Screens(baseConfig).ValidateLinksFooter(page, testResults, selectedEnvironment);
            await new Screens(baseConfig).ValidateLinksHeader(page, testResults, selectedEnvironment);
            await new Screens(baseConfig).ValidateLinksMyOrders(page, testResults, selectedEnvironment);
            await new Screens(baseConfig).ValidateLinksMyData(page, testResults, selectedEnvironment);
            await new Screens(baseConfig).ValidateProductDetails(page, testResults, selectedEnvironment);
        }
    };

    foreach (var action in actions)
    {
        // Executa todas as ações se "Executar Todos" for selecionado
        if (selectedMenu == "Executar Todos" || selectedMenu == action.Key)
        {
            Console.WriteLine($"Executando: {action.Key}");
            try
            {
                await action.Value(); // Executa a ação
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar {action.Key}: {ex.Message}");
            }
        }
    }

    Console.WriteLine("\nExecução concluída!");

    // Gera o relatório
    Report.GenerateHtmlReport(testResults);

    // Fecha o navegador após a execução
    await browser.CloseAsync();

}
else
{
    Console.WriteLine("\nOpção inválida! Finalizando o programa.");
}



















