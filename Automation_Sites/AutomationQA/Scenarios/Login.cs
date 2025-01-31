using Microsoft.Playwright;
using System.Diagnostics;


namespace AutomationQA.Scenarios;

internal class Login 
{
    private Dictionary<string, (string Login, string Senha)> credentials = new Dictionary<string, (string, string)>
    {
        { "UAT", ("45926744806", "Teste@123") },
        { "RC", ("47498928825", "Test123!") },
        { "PROD", ("teste@prod.com.br", "senha3") }
    };

    public async Task GoToCredentials(IPage page, List<TestResult> results, string selectedEnvironment) // acessar pagina de login
    {
        await page.Locator("text= Acesse ou Cadastre-se").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await page.Locator("text= Acesse ou Cadastre-se").ClickAsync();
    }


    public async Task ValidateLoginCorrect(IPage page, List<TestResult> results, string selectedEnvironment, bool RegisterResults = true)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await GoToCredentials(page, results, selectedEnvironment);
            var (login, senha) = SetUser(selectedEnvironment);  // Obtém o login e a senha

            var user = page.Locator("id=loginEmail");
            await user.FillAsync(login);

            // Preenche o campo de senha
            var password = page.Locator("xpath=//input[@name='password']");
            await password.FillAsync(senha);

            // Clique no botão de enviar ou "Login"
            var submitButton = page.Locator("id=btnLogin");
            await submitButton.ClickAsync();
            await Task.Delay(5000);

            stopwatch.Stop();
            if (RegisterResults)
            {
                results.Add(new TestResult("Login Credenciais Corretas", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
            }
        }
        catch (Exception ex)
        {
            if (RegisterResults)
            {
                // Captura falhas e registra nos resultados
                Console.WriteLine($"Login Credenciais Corretas -> FAIL: {ex.Message} \n");
                results.Add(new TestResult("Login Credenciais Corretas", $"FAIL - {ex.Message}"));
            }
        }
    }

    public async Task ValidateLoginIncorrect(IPage page, List<TestResult> results, string selectedEnvironment)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await GoToCredentials(page, results, selectedEnvironment);
            // Chama SetUser para obter o login e a senha correspondentes ao ambiente
            var (login, senha) = SetUser(selectedEnvironment);  // Obtém o login e a senha

            var user = page.Locator("id=loginEmail");
            await user.FillAsync("123456789");

            // Preenche o campo de senha
            var password = page.Locator("xpath=//input[@name='password']");
            await password.FillAsync(senha);

            // Clique no botão de enviar ou "Login"
            var submitButton = page.Locator("id=btnLogin");
            await submitButton.ClickAsync();
            await Task.Delay(5000);

            // Aguarda o elemento da header estar visível
            await page.Locator("id=loginForm_server_validation").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            // Se chegou até aqui sem exceções, o teste passou
            stopwatch.Stop();
            results.Add(new TestResult("Login Credenciais Incorretas", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            // Captura falhas e registra nos resultados
            Console.WriteLine($"Login Credenciais Incorretas -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Login Credenciais Incorretas", $"FAIL - {ex.Message}"));
        }
    }

    public async Task LogOff(IPage page)
    {
        await page.Locator("i[class='fa fa-sign-out']").ClickAsync();
        await page.Locator("text= Acesse ou Cadastre-se").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
    }

    public (string Login, string Senha) SetUser(string selectedEnvironment)
    {
        // Verifica se o ambiente está no dicionário
        if (!credentials.ContainsKey(selectedEnvironment))
        {
            throw new ArgumentException("Ambiente não reconhecido!");
        }

        // Retorna CPF e senha correspondentes ao ambiente
        return credentials[selectedEnvironment];
    }

}