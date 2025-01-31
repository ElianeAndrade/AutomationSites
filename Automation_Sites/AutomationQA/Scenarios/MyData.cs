using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Diagnostics;

namespace AutomationQA.Scenarios;
internal class MyData
{
    public async Task ClickMyDataAndAdress(IPage page)
    {

        await page.Locator("button[class=\"dropbtn havanna-icons\"]").HoverAsync(); 
        await page.Locator("id=btn-meus-dados").ClickAsync(); // Clique em Meus Dados no Header
        await page.Locator("css=input[id='firstNameTextBox']").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
    }

    public async Task ValidateInvalidDateOfBirth(IPage page, List<TestResult> results, string selectedEnvironment, Dictionary<string, string> dados)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await new Login().ValidateLoginCorrect(page, results, selectedEnvironment, false);
            await new MyData().ClickMyDataAndAdress(page);
            await page.Locator("id=birthDateTextBox").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("id=birthDateTextBox").ClickAsync();
            await page.Keyboard.PressAsync("Control+A");            
            await page.Keyboard.PressAsync("Backspace");           
            await page.Keyboard.TypeAsync("05072008");
            await page.Locator("id=btnSalvarContato").ClickAsync();
            await Task.Delay(10000);
            await page.Locator("xpath=//div[@class='is-completed form-group has-error'][contains(.,'* Idade mínima: 18 ano(s)')]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Validar Data de Nacimento - Menor de 18 anos", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Validar Data de Nacimento - Menor de 18 anos -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Validar Data de Nacimento - Menor de 18 anos", $"FAIL - {ex.Message}"));
        }
    }

    //Preenchimento Manual
    public async Task CEPManualFilling(IPage page, List<TestResult> results, string selectedEnvironment, Dictionary<string, string> dados)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            //Clica no icone do endereço e inicia o processo de cadastro de endereço
            await page.Locator("css=i[class=\"fa fa-map-marker fa-2x\"]").ClickAsync(); // Clique no ícone de endereço
            await page.Locator("id=btnAdicionarEndereco").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("id=btnAdicionarEndereco").ClickAsync();
            await page.Locator("id=postalCodeTextBox").ClickAsync();
            await page.Keyboard.TypeAsync("99680000");
            await page.Locator("id=streetNameTextBox").ClickAsync();
            await page.Keyboard.TypeAsync(dados["rua"]);
            await page.Locator("id=districtTextBox").ClickAsync();
            await page.Keyboard.TypeAsync(dados["bairro"]);
            await page.Locator("id=numberTextBox").ClickAsync();
            await page.Keyboard.TypeAsync(dados["logradouro"]);
            await page.Locator("css=input[name='Address.Description']").FillAsync(dados["desc"]);
            await page.Locator("id=btnSalvarContato").ClickAsync();
            await new Login().LogOff(page);

            stopwatch.Stop();
            results.Add(new TestResult("Fluxo CEP Sem Preenchimento Automático", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fluxo CEP Sem Preenchimento Automático-> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Fluxo CEP Sem Preenchimento Automático", $"FAIL - {ex.Message}"));
        }
    }

    //Validar Complemento obrigatorio
    public async Task ValidateComplement(IPage page, List<TestResult> results, string selectedEnvironment, Dictionary<string, string> dados)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await new Login().ValidateLoginCorrect(page, results, selectedEnvironment, false);
            await new MyData().ClickMyDataAndAdress(page);
            //Clica no icone do endereço e inicia o processo de cadastro de endereço
            await page.Locator("css=i[class=\"fa fa-map-marker fa-2x\"]").ClickAsync(); // Clique no ícone de endereço
            await page.Locator("id=btnAdicionarEndereco").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("id=btnAdicionarEndereco").ClickAsync();
            await page.Locator("id=postalCodeTextBox").ClickAsync();
            await page.Keyboard.TypeAsync(dados["cep"]);
            await page.Locator("xpath=//span[contains(@class,'switch-lever')]").ClickAsync();
            await page.Locator("css=input[name='Address.Description']").FillAsync(dados["desc"]);
            await page.Locator("id=btnSalvarContato").ClickAsync();

            // Validar mensagem de campo obrigatório
            await page.Locator("xpath=//label[@for='complementTextBox'][contains(.,'Este campo é obrigatório.')]")
                .WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
            await new Login().LogOff(page);

            stopwatch.Stop();
            results.Add(new TestResult("Complemento Obrigatório", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Complemento Obrigatório -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Complemento Obrigatório", $"FAIL - {ex.Message}"));
        }
    }

    public async Task EditAddress(IPage page, List<TestResult> results, string selectedEnvironment, Dictionary<string, string> dados)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await new Login().ValidateLoginCorrect(page, results, selectedEnvironment, false);
            await new MyData().ClickMyDataAndAdress(page);
            //Clica no icone do endereço 
            await page.Locator("css=i[class=\"fa fa-map-marker fa-2x\"]").ClickAsync(); // Clique no ícone de endereço
            await page.Locator("id=btnAdicionarEndereco").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("id=btnAdicionarEndereco").ClickAsync();
            await new Registration().FillAddress(page, results, dados);

            // Esperar botão de edição ser visível e clicar
            await page.Locator("xpath=//div[@class='row row-top-space']/div[@class='col-md-12']//tbody[@class='color-black']/tr[2]/td[@data-title='Ações']/a[1]")
                .WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });

            await page.Locator("xpath=//div[@class='row row-top-space']/div[@class='col-md-12']//tbody[@class='color-black']/tr[2]/td[@data-title='Ações']/a[1]")
                .ClickAsync();

            // Editar endereço
            await page.Locator("css=input[name='Address.Description']").FillAsync("EDIÇÃO ENDEREÇO");

            await page.Locator("xpath=(//span[contains(@class,'switch-lever')])[2]").ClickAsync();
            await page.Locator("id=btnSalvarContato").ClickAsync();

            stopwatch.Stop();
            results.Add(new TestResult("Criar e Editar Endereço", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Criar e Editar Endereço -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Criar e Editar Endereço", $"FAIL - {ex.Message}"));
        }
    }

    public async Task DeleteAddress(IPage page, List<TestResult> results, string selectedEnvironment)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            // Clicar para excluir endereço
            await page.Locator("xpath=//div[@class='row row-top-space']/div[@class='col-md-12']//tbody[@class='color-black']/tr[2]/td[@data-title='Ações']/a[2]")
                .ClickAsync();

            // Confirmar exclusão
            await page.Locator("xpath=//button[@type='button'][contains(.,'Sim')]").WaitForAsync();
            await page.Locator("xpath=//button[@type='button'][contains(.,'Sim')]").ClickAsync();

            // Esperar o botão de adicionar endereço ficar visível novamente
            await page.Locator("id=btnAdicionarEndereco")
                .WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
            await new Login().LogOff(page);

            stopwatch.Stop();
            results.Add(new TestResult("Excluir Endereço", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excluir Endereço -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Excluir Endereço", $"FAIL - {ex.Message}"));
        }
    }
}
