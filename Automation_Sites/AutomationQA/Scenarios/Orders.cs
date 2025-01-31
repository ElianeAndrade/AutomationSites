using AutomationQA.GenericActions;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Diagnostics;

namespace AutomationQA.Scenarios;

internal class Orders
{
    private readonly BaseConfig _baseConfig;

    public Orders(BaseConfig baseConfig)
    {
        _baseConfig = baseConfig;
    }

    //Procura o produto através da barra de busca
    public async Task SearchAvailableProduct(IPage page, List<TestResult> results, string selectedEnvironment)
    {
        string skuProductSelected = await _baseConfig.SearchProduct();
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await page.Locator("xpath =//input[@id='q-header-desktop']").FillAsync(skuProductSelected);
            await page.Locator("xpath=//button[@id=\"SearchButtonDesktop\"]").ClickAsync();
            await Task.Delay(3000);

            stopwatch.Stop();
            //results.Add(new TestResult("Validação de disponibilidade do produto", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Validação de disponibilidade do produto -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Validação de disponibilidade do produto", $"FAIL - {ex.Message}"));
        }

    }
    //Clica em comprar o produto
    public async Task ClickBuy(IPage page, List<TestResult> results)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await page.Locator("button[class='block-ui produto-comprar-btn']").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("button[class='block-ui produto-comprar-btn']").ClickAsync();
            stopwatch.Stop();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Validação de disponibilidade do produto -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Validação de disponibilidade do produto", $"FAIL - {ex.Message}"));
        }
    }

    //Adiciona o produto no carrinho - na tela de detalhes do produto
    public async Task AddProductCart(IPage page, List<TestResult> results)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await page.Locator("a[title=\"Adicionar ao Carrinho\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("a[title=\"Adicionar ao Carrinho\"]").ClickAsync();
            await page.Locator("text = Carrinho de compras").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Adicionar Produto no Carrinho -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Adicionar Produto no Carrinho", $"FAIL - {ex.Message}"));
        }
    }

    //Clica em fechar pedido - dentro do carrinho de compras
    public async Task CloseOrder(IPage page, List<TestResult> results)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await page.Locator("text=Fechar pedido").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("id=checkoutButtom").ClickAsync();

            stopwatch.Stop();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Clique no botão Fechar Pedido -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Clique no botão Fechar Pedido", $"FAIL - {ex.Message}"));
        }
    }
    //Valida a tela de verificação do produto
    public async Task ValidateVerificationScreen(IPage page, List<TestResult> results)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await page.Locator("xpath=//*[@class='box-header']/h3[contains(text(), 'Verificação')]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("id=goToShippingLink").ClickAsync();
            stopwatch.Stop();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Verificação do Produto -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Verificação do Produto", $"FAIL - {ex.Message}"));
        }
    }

    //Valida a tela de Entrega do Produto
    public async Task ValidateDeliveryScreen(IPage page, List<TestResult> results, string optionDelivery, Dictionary<string, string> dados)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            if (optionDelivery == "Default Address")
            {
                await page.Locator("xpath=//*[@class='box-header']/h3[contains(text(), 'Quero que me entregue')]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
                await page.Locator("id=goToPaymentLink").ClickAsync();
                stopwatch.Stop();
            }
            if (optionDelivery == "New Adress")
            {
                await page.Locator("//*[@class='box-header']/h3[contains(text(), 'Quero que me entregue')]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
                await page.Locator("a[class='btn btn-default btn-sm btn-block-xs']").ClickAsync();
                await page.Locator("i[class='fa fa-plus']").ClickAsync();
                await new Registration().FillAddressNewAdress(page, results, dados);
                await page.Locator("id=goToPaymentLink").ClickAsync();
                stopwatch.Stop();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Verificação da Entrega do Produto -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Verificação da Entrega do Produto", $"FAIL - {ex.Message}"));
        }
    }

    //Pagar com boleto Bancário
    public async Task PayWithBankSlip(IPage page, List<TestResult> results, string selectedEnvironment, Dictionary<string, string> dados)
    {
        
        await new Orders(_baseConfig).SearchAvailableProduct(page, results, selectedEnvironment); // procura pedido
        await new Orders(_baseConfig).ClickBuy(page, results);
        await new Orders(_baseConfig).AddProductCart(page, results);
        await new Orders(_baseConfig).CloseOrder(page, results);
        await new Login().ValidateLoginCorrect(page, results, selectedEnvironment, false);
        await page.Locator("a[id='btn-carrinho']").ClickAsync(); // Carrinho de Compras
        await new Orders(_baseConfig).CloseOrder(page, results);
        await new Orders(_baseConfig).ValidateVerificationScreen(page, results);
        await new Orders(_baseConfig).ValidateDeliveryScreen(page, results, "Default Address", dados);

        var stopwatch = Stopwatch.StartNew();
        try
        {
            await Task.Delay(3000);
            await page.Locator("id=panel-boleto").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("id=panel-boleto").ClickAsync();
            // Aguarda processo manual ou continuar com o fluxo
            //await WaitManualAction.WaitForManualAction(page);
            await page.Locator("id=btn-pagar-boleto").ClickAsync();
            await Task.Delay(3000);
            await page.Locator("xpath=//*[@class='text-primary'][contains(text(), 'PEDIDO')]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("id=botaoImprimir").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await new Login().LogOff(page);

            stopwatch.Stop();
            results.Add(new TestResult("Pagamento realizado com Boleto", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Pagamento realizado com Boleto -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Pagamento realizado com Boleto", $"FAIL - {ex.Message}"));
        }
    }

    public async Task PaymentAndChangeOfAddress(IPage page, List<TestResult> results, string selectedEnvironment, Dictionary<string, string> dados)
    {
        
        await new Orders(_baseConfig).SearchAvailableProduct(page, results, selectedEnvironment); // procura pedido
        await new Orders(_baseConfig).ClickBuy(page, results);
        await new Orders(_baseConfig).AddProductCart(page, results);
        await new Orders(_baseConfig).CloseOrder(page, results);
        await new Login().ValidateLoginCorrect(page, results, selectedEnvironment, false);
        await page.Locator("a[id='btn-carrinho']").ClickAsync(); // Clique para voltar ao carrinho de Compras
        await new Orders(_baseConfig).CloseOrder(page, results);
        await new Orders(_baseConfig).ValidateVerificationScreen(page, results);
        await new Orders(_baseConfig).ValidateDeliveryScreen(page, results, "New Adress", dados);

        var stopwatch = Stopwatch.StartNew();
        try
        {
            await Task.Delay(3000);
            await page.Locator("id=panel-boleto").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("id=panel-boleto").ClickAsync();
            // Aguarda processo manual ou continuar com o fluxo
            //await WaitManualAction.WaitForManualAction(page);
            await page.Locator("id=btn-pagar-boleto").ClickAsync();
            await Task.Delay(10000);
            await page.Locator("xpath=//*[@class='text-primary'][contains(text(), 'PEDIDO')]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("id=botaoImprimir").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await new Login().LogOff(page);

            stopwatch.Stop();
            results.Add(new TestResult("Pagamento realizado com Boleto - Troca de Endereço", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Pagamento realizado com Boleto - Troca de Endereço-> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Pagamento realizado com Boleto - Troca de Endereço", $"FAIL - {ex.Message}"));
        }
    }

    public async Task PayWithPix(IPage page, List<TestResult> results, string selectedEnvironment, Dictionary<string, string> dados)
    {
        await new Orders(_baseConfig).SearchAvailableProduct(page, results, selectedEnvironment); // procura pedido
        await new Orders(_baseConfig).ClickBuy(page, results);
        await new Orders(_baseConfig).AddProductCart(page, results);
        await new Orders(_baseConfig).CloseOrder(page, results);
        await new Login().ValidateLoginCorrect(page, results, selectedEnvironment, false);
        await page.Locator("a[id='btn-carrinho']").ClickAsync(); // Carrinho de Compras
        await new Orders(_baseConfig).CloseOrder(page, results);
        await new Orders(_baseConfig).ValidateVerificationScreen(page, results);
        await new Orders(_baseConfig).ValidateDeliveryScreen(page, results, "Default Address", dados);

        var stopwatch = Stopwatch.StartNew();
        try
        {
            await Task.Delay(3000);
            await page.Locator("xpath=//h2[@class='panel-title'][contains(.,'PIX')]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("xpath=//h2[@class='panel-title'][contains(.,'PIX')]").ClickAsync();
            // Aguarda processo manual ou continuar com o fluxo
            //await WaitManualAction.WaitForManualAction(page);
            await page.Locator("id=btn-pagar-pix").ClickAsync();
            await Task.Delay(10000);
            //Validar Pop-Up PIX
            await page.Locator("button[class=\"btn btn-default btn-block-xs\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 20000 });
            await page.Locator("button[class=\"btn btn-default btn-block-xs\"]").ClickAsync(); 
            await Task.Delay(5000);
            await page.Locator("xpath=//*[@class='text-primary'][contains(text(), 'PEDIDO')]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await new Login().LogOff(page);

            stopwatch.Stop();
            results.Add(new TestResult("Pagamento realizado com Pix", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Pagamento realizado com Pix -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Pagamento realizado com Pix", $"FAIL - {ex.Message}"));
        }
    }

    public async Task PayWithCreditCard(IPage page, List<TestResult> results, string selectedEnvironment, Dictionary<string, string> dados)
    {
        await new Orders(_baseConfig).SearchAvailableProduct(page, results, selectedEnvironment); // procura pedido
        await new Orders(_baseConfig).ClickBuy(page, results);
        await new Orders(_baseConfig).AddProductCart(page, results);
        await new Orders(_baseConfig).CloseOrder(page, results);
        await new Login().ValidateLoginCorrect(page, results, selectedEnvironment, false);
        await page.Locator("a[id='btn-carrinho']").ClickAsync(); // Carrinho de Compras
        await new Orders(_baseConfig).CloseOrder(page, results);
        await new Orders(_baseConfig).ValidateVerificationScreen(page, results);
        await new Orders(_baseConfig).ValidateDeliveryScreen(page, results, "Default Address", dados);

        var stopwatch = Stopwatch.StartNew();
        try
        {
            await Task.Delay(3000);
            await page.Locator("id=panel-credit").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("id=panel-credit").ClickAsync();
            await page.Locator("xpath=(//span[contains(.,'-- Selecione --')])[1]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("xpath=(//a[@class='chosen-single'][contains(.,'-- Selecione --')])[1]").ClickAsync();
            await page.Locator("xpath=//*[@class='chosen-results']/li[contains(text(), 'MasterCard')]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("xpath=//*[@class='chosen-results']/li[contains(text(), 'MasterCard')]").ClickAsync();

            await page.Locator("id=cardQuotasDropDown_chosen").ClickAsync();
            await page.Locator("xpath=//*[@class='chosen-results']/li[contains(text(), '1x')]").ClickAsync();
            await page.Locator("id=CardNumber").FillAsync("000000001");
            await page.Locator("id=CardHolderName").FillAsync("Automação");
            await page.Locator("id=CardExpirationMonth").FillAsync("12");
            await page.Locator("id=CardExpirationYear").FillAsync("2027");
            await page.Locator("id=CardSecurityCode").FillAsync("258");
            // Aguarda processo manual ou continuar com o fluxo
            //await WaitManualAction.WaitForManualAction(page);
            await page.Locator("id=btn-pagar-credito").ClickAsync();
            await Task.Delay(30000);
            await page.Locator("xpath=//*[@class='text-primary'][contains(text(), 'PEDIDO')]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await new Login().LogOff(page);

            stopwatch.Stop();
            results.Add(new TestResult("Pagamento realizado com Cartão de Crédito", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Pagamento realizado com Cartão de Crédito -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Pagamento realizado com Cartão de Crédito", $"FAIL - {ex.Message}"));
        }
    }
}

