using AutomationQA.GenericActions;
using Microsoft.Playwright;

namespace AutomationQA.MenusEnvironments;

internal class Environments : Welcome
{

    private Dictionary<int, string> environment = new Dictionary<int, string>
    {
        { 1, "UAT" },
        { 2, "RC" },
        { 3, "PRODUÇÃO" }
    };

    public string AskEnvironment()
    {
        int choice = 0;

        while (true)
        {
            Console.Clear();
            Presentation("AMBIENTE");
            Console.WriteLine("Ambientes Disponíveis:");

            foreach (var env in environment)
            {
                Console.WriteLine($"{env.Key} - {env.Value}");
            }

            Console.Write("\nDigite o número correspondente ao ambiente desejado: ");

            if (int.TryParse(Console.ReadLine(), out choice) && environment.ContainsKey(choice))
            {
                // Retorna o ambiente selecionado caso válido
                string selectedEnvironment = environment[choice];
                return selectedEnvironment;
            }

            // Se inválido, exibe mensagem de erro
            Console.WriteLine("\nEscolha inválida. Pressione qualquer tecla para tentar novamente.");
            Console.ReadKey();
        }
    }

    public async Task<(IBrowser, IPage)> UrlEnvironment(string selectedEnvironment)
    {
        var playwright = await Playwright.CreateAsync();

        // Inicia o navegador e abre a página após a escolha da classe
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe",
            Headless = false,
            SlowMo = 2000
        });
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();


        try
        {
            switch (selectedEnvironment)
            {
                case "UAT":
                    await page.GotoAsync("https://uat.satelital.com.br/havanna", new PageGotoOptions
                    {
                        Timeout = 60000, 
                        WaitUntil = WaitUntilState.NetworkIdle // Espera até que a rede esteja ociosa
                    });
                    Console.WriteLine("Acessando página UAT da campanha!");
                    break;
                case "RC":
                    await page.GotoAsync("https://rc.satelital.com.br/havanna", new PageGotoOptions
                    {
                        Timeout = 60000, 
                        WaitUntil = WaitUntilState.NetworkIdle 
                    });
                    Console.WriteLine("Acessando página RC da campanha!");
                    break;
                case "PRODUÇÃO":
                    await page.GotoAsync("https://prod.satelital.com.br/havanna", new PageGotoOptions
                    {
                        Timeout = 60000, 
                        WaitUntil = WaitUntilState.NetworkIdle 
                    });
                    Console.WriteLine("Acessando página PRODUÇÃO da campanha!");
                    break;
                default:
                    Console.WriteLine("Ambiente inválido. Nenhuma URL foi acessada.");
                    break;
            }
        }
        catch (TimeoutException)
        {
            Console.WriteLine($"Erro: O carregamento da página para o ambiente '{selectedEnvironment}' excedeu o tempo limite.");
        }
        return (browser, page);
    }

}
