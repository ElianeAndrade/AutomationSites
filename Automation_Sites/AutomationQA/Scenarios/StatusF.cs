using AutomationQA.GenericActions;
using AutomationQA.Scenarios;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Diagnostics;
public class StatusF
{
    private readonly BaseConfig _baseConfig;

    public StatusF(BaseConfig baseConfig)
    {
        _baseConfig = baseConfig;
    }

    
    // Método principal para realizar o processo
    public async Task ValidadeStatusF(IPage page, List<TestResult> results, Dictionary<string, string> dados, string selectedEnvironment)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            // Busca o parDocumentId no banco de dados
            string parDocumentId = await _baseConfig.SearchParDocumentId();
            await new Login().GoToCredentials(page, results, selectedEnvironment);
            // Preenche os campos de login na página (substituindo pelo parDocumentId)
            await page.Locator("id=loginEmail").FillAsync(parDocumentId);
            Login setUserLogin = new();
            var (login, senha) = setUserLogin.SetUser(selectedEnvironment); 
            await page.Locator("xpath=//input[@name='password']").FillAsync(senha);
            await page.Locator("id=btnLogin").ClickAsync();
            await Task.Delay(3000);

            // Aguarda e interage com a página após o login
            await page.Locator("css=div[class='modal-footer'] button[class='btn btn-primary']").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("css=div[class='modal-footer'] button[class='btn btn-primary']").ClickAsync();
            // Aguarda processo manual ou continuar com o fluxo
            //await WaitManualAction.WaitForManualAction(page);
            await page.Locator("css=div[class='pull-right'] button[class='btn btn-primary']").ClickAsync();
            await Task.Delay(3000);
            await new Registration().FillAddress(page, results, dados);
            await new Registration().AcceptTerms(page, results);
            await new Login().LogOff(page);

            stopwatch.Stop();
            results.Add(new TestResult("Status F", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Status F -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Status F", $"FAIL - {ex.Message}"));
        }
    }
}

