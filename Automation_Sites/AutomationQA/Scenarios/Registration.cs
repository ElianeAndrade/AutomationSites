using AutomationQA.FakeDatas;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Diagnostics;

namespace AutomationQA.Scenarios;

internal class Registration
{
    public async Task Register(IPage page, List<TestResult> results, string tipoCadastro, string selectedEnvironment)
    {
        // Chama a classe FakeDataGenerator para obter os dados fakers
        var dados = FakeDataGenerator.GenerateDataFalse(tipoCadastro);

        // Preencher o formulário de cadastro
        await new Login().GoToCredentials(page, results, selectedEnvironment);

        if (tipoCadastro == "CPF")
        {
            await page.Locator("id=registerEmail").FillAsync(dados["documento"]);
            await page.Locator("id=btn-cadastrar-usuario").ClickAsync();
            await Task.Delay(2000);
            await RegisterCPF(page, results, dados);

        }
        else if (tipoCadastro == "CNPJ")
        {
            await page.Locator("id=registerEmail").FillAsync(dados["documento"]);
            await page.Locator("id=btn-cadastrar-usuario").ClickAsync();
            await Task.Delay(2000);
            await RegisterCNPJ(page, results, dados);
        }
        else if (tipoCadastro == "EMAIL")
        {
            await page.Locator("id=registerEmail").FillAsync(dados["email"]);
            await page.Locator("id=btn-cadastrar-usuario").ClickAsync();
            await Task.Delay(2000);
            await RegisterEmail(page, results, dados);
        }
    }

    // Método para preencher dados de CPF
    private async Task RegisterCPF(IPage page, List<TestResult> results, Dictionary<string, string> dados)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await page.Locator("id=firstNameTextBox").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
            //await WaitManualAction.WaitForManualAction(page);
            await page.Locator("id=firstNameTextBox").FillAsync(dados["nome"]);
            await page.Locator("id=lastNameTextBox").FillAsync(dados["sobrenome"]);
            await page.Locator("(//input[@name='Gender']/../span[@class='radio-checkmark'])[2]").ClickAsync(); // Assumindo que é o segundo botão de gênero (F)
            await page.Locator("id=emailTextBox").FillAsync(dados["email"]);
            await page.Locator("id=confirmEmailTextBox").FillAsync(dados["email"]);
            await page.Locator("id=phone2TextBox").FillAsync(dados["celular"]);
            await page.Locator("id=passwordTextBox").FillAsync(dados["senha"]);
            await page.Locator("id=confirmPasswordTextBox").FillAsync(dados["senha"]);
            await page.Locator("id=birthDateTextBox").ClickAsync();
            await page.Keyboard.TypeAsync(dados["nascimento"]);
            await page.Locator("id=btnSalvarContato").ClickAsync();
            await FillAddress(page, results, dados);
            await AcceptTerms(page, results);
            await new Login().LogOff(page);

            stopwatch.Stop();
            results.Add(new TestResult("Fluxo de Cadastro CPF", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fluxo de Cadastro CPF-> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Fluxo de Cadastro CPF", $"FAIL - {ex.Message}"));
        }
    }

    // Método para preencher dados de CNPJ
    private async Task RegisterCNPJ(IPage page, List<TestResult> results, Dictionary<string, string> dados)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await page.Locator("id=NameTextBox").FillAsync(dados["nome"]);
            await page.Locator("id=emailTextBox").FillAsync(dados["email"]);
            await page.Locator("id=confirmEmailTextBox").FillAsync(dados["email"]);
            await page.Locator("id=InscricaoEstadualTextBox").FillAsync("300847939611");
            await page.Locator("xpath=(//span[contains(@class,'switch-lever')])[1]").ClickAsync();
            await page.Locator("id=phone2TextBox").FillAsync(dados["celular"]);
            await page.Locator("id=passwordTextBox").FillAsync(dados["senha"]);
            await page.Locator("id=confirmPasswordTextBox").FillAsync(dados["senha"]);
            await page.Locator("id=btnSalvarContato").ClickAsync();
            await FillAddress(page, results, dados);
            await AcceptTerms(page, results);
            await new Login().LogOff(page);

            stopwatch.Stop();
            results.Add(new TestResult("Fluxo de Cadastro CNPJ", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            // Captura falhas e registra nos resultados
            Console.WriteLine($"Fluxo de Cadastro CNPJ -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Fluxo de Cadastro CNPJ", $"FAIL - {ex.Message}"));
        }
    }

    // Método para preencher dados de EMAIL
    private  async Task RegisterEmail(IPage page, List<TestResult> results, Dictionary<string, string> dados)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await page.Locator("id=firstNameTextBox").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
            await page.Locator("id=firstNameTextBox").FillAsync(dados["nome"]);
            await page.Locator("id=lastNameTextBox").FillAsync(dados["sobrenome"]);
            await page.Locator("(//input[@name='Gender']/../span[@class='radio-checkmark'])[2]").ClickAsync();
            await page.Locator("id=cpfTextbox").FillAsync(dados["documento"]);
            await page.Locator("id=confirmEmailTextBox").FillAsync(dados["email"]);
            await page.Locator("id=phone2TextBox").FillAsync(dados["celular"]);
            await page.Locator("id=passwordTextBox").FillAsync(dados["senha"]);
            await page.Locator("id=confirmPasswordTextBox").FillAsync(dados["senha"]);
            await page.Locator("id=birthDateTextBox").ClickAsync();
            await page.Keyboard.TypeAsync(dados["nascimento"]);  
            await page.Locator("id=btnSalvarContato").ClickAsync();
            await FillAddress(page, results, dados);
            await AcceptTerms(page, results);
            await new Login().LogOff(page);

            stopwatch.Stop();
            results.Add(new TestResult("Fluxo de Cadastro EMAIL", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fluxo de Cadastro EMAIL -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Fluxo de Cadastro EMAIL ", $"FAIL - {ex.Message}"));
        }
    }

    public async Task FillAddress(IPage page, List<TestResult> results, Dictionary<string, string> dados)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await page.Locator("id=postalCodeTextBox").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
            await page.Locator("id=postalCodeTextBox").ClickAsync();
            await page.Keyboard.TypeAsync(dados["cep"]);
            await Task.Delay(5000);
            await page.Locator("id=numberTextBox").FillAsync(dados["logradouro"]);
            await page.Locator("id=complementTextBox").FillAsync(dados["comp"]);
            await page.Locator("css=input[name='Address.Description']").FillAsync(dados["desc"]);
            await page.Locator("id=btnSalvarContato").ClickAsync();


            stopwatch.Stop();
            //results.Add(new TestResult("Fluxo de Preencher Endereço", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fluxo de Preencher Endereço > FAIL: {ex.Message} \n");
            results.Add(new TestResult("Fluxo de Preencher Endereço", $"FAIL - {ex.Message}"));
        }
    }

    public async Task AcceptTerms(IPage page, List<TestResult> results)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await Task.Delay(5000);
            await page.Locator("(//span[contains(@class,'checkbox-checkmark')])[1]").ClickAsync();
            await page.Locator("(//span[contains(@class,'checkbox-checkmark')])[2]").ClickAsync();
            await page.Locator("id=AcceptButton").ClickAsync();
            await Task.Delay(5000);

            stopwatch.Stop();
            //results.Add(new TestResult("Fluxo de Aceitar Termos", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fluxo de Aceitar Termos -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Fluxo de Aceitar Termos", $"FAIL - {ex.Message}"));
        }
    }

    public async Task UserRegistered(IPage page, List<TestResult> results, string selectedEnvironment)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await new Login().GoToCredentials(page, results, selectedEnvironment);
            await page.Locator("id=registerEmail").FillAsync("45926744806");
            await page.Locator("id=btn-cadastrar-usuario").ClickAsync();
            await page.Locator("div[id='registerForm_server_validation'] ").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Fluxo de Usuário Já Cadastrado", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fluxo de Usuário Já Cadastrado -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Fluxo de Usuário Já Cadastrado", $"FAIL - {ex.Message}"));
        }

    }

    public async Task FillAddressNewAdress(IPage page, List<TestResult> results, Dictionary<string, string> dados)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await page.Locator("id=postalCodeTextBox").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
            await page.Locator("id=postalCodeTextBox").ClickAsync();
            await page.Keyboard.TypeAsync(dados["cep"]);
            await Task.Delay(5000);
            await page.Locator("id=numberTextBox").FillAsync(dados["logradouro"]);
            await page.Locator("id=complementTextBox").FillAsync(dados["comp"]);
            await page.Locator("css=input[name='Address.Description']").FillAsync(dados["desc"]);
            await page.Locator("div[class=\"box-footer\"] button[class=\"btn btn-primary\"]").ClickAsync();


            stopwatch.Stop();
            //results.Add(new TestResult("Fluxo de Preencher Endereço - Troca de Endereço ", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fluxo de Preencher Endereço - Troca de Endereço > FAIL: {ex.Message} \n");
            results.Add(new TestResult("Fluxo de Preencher Endereço - Troca de Endereço ", $"FAIL - {ex.Message}"));
        }
    }
}
