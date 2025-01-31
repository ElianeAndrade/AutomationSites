using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Diagnostics;


namespace AutomationQA.Scenarios;

internal class OpenSupport
{
    public async Task ValidateOpenSupport(IPage page, List<TestResult> results, string selectedEnvironment)
    {
        await new Login().ValidateLoginCorrect(page, results, selectedEnvironment, false);
        var stopwatch = Stopwatch.StartNew();
        try
        {

            // Aguarda o elemento da header estar visível
            await page.Locator("ul[class='footer-list footer-list-info text-center'] a[href=\"/havanna/CustomerService/OpenTicket\"]").ClickAsync();
            await page.Locator("div[class='box-header']").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            // Clique nos dropdowns para selecionar valores
            await page.Locator("div[id='motivoDropDown_chosen']").ClickAsync();
            await page.Locator("li[data-option-array-index='1']").ClickAsync();
            await page.Locator("div[id='categoriesDropDown_chosen']").ClickAsync();
            await page.Locator("//li[contains(text(), 'Dúvida(s)')]").ClickAsync();
            await page.Locator("div[id='subcategoriesDropDown_chosen']").ClickAsync();
            await page.Locator("//li[contains(text(), 'Serviço')]").ClickAsync();

            // Preenche o texto na área de texto
            var textArea = page.Locator("textarea[class='form-control required']");
            await textArea.FillAsync("Validação realizada pela automação");

            // Marca os checkboxes
            await page.Locator("(//span[contains(@class,'checkbox-checkmark')])[1]").ClickAsync();
            await page.Locator("(//span[contains(@class,'checkbox-checkmark')])[2]").ClickAsync();

            // Submete o formulário
            await page.Locator("button[type='submit']").ClickAsync();

            // Aguarda o próximo estado da página
            await page.Locator("div[class='box-header']").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });

            // Clica no botão padrão para seguir
            await page.Locator("a[class='btn btn-default btn-block-xs']").ClickAsync();

            // Aguarda a próxima header ficar visível
            await page.Locator("div[class='box-header'] h3").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            await new Login().LogOff(page);

            stopwatch.Stop();
            results.Add(new TestResult("Abrir Chamado", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Abrir Chamado -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Abrir Chamado", $"FAIL - {ex.Message}"));
        }
    }

    public async Task SupportDetails(IPage page, List<TestResult> results, string selectedEnvironment)
    {
        await new Login().ValidateLoginCorrect(page, results, selectedEnvironment, false);
        var stopwatch = Stopwatch.StartNew();

        try
        {
            // Aguarda o elemento da header estar visível
            await page.Locator("ul[class='footer-list footer-list-info text-center'] a[href=\"/havanna/CustomerService/ListTickets\"]").ClickAsync();
            await page.Locator("div[class='box-header']").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("(//td[@class=\"text-right col-details\"]/a[contains(@class, \"btn-primary\")])[1]").ClickAsync();
            await page.Locator("text=Dados do Chamado").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 2000 });
            await new Login().LogOff(page);

            stopwatch.Stop();
            results.Add(new TestResult("Detalhes do Chamado", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            // Captura falhas e registra nos resultados
            Console.WriteLine($"Detalhes do Chamado -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Detalhes do Chamado", $"FAIL - {ex.Message}"));
        }
    }
}
