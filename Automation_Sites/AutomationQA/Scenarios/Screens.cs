using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Diagnostics;

namespace AutomationQA.Scenarios;

internal class Screens
{
    private readonly BaseConfig _baseConfig;

    public Screens(BaseConfig baseConfig)
    {
        _baseConfig = baseConfig;
    }
    public async Task ValidateLinksFooter(IPage page, List<TestResult> results, string selectedEnvironment)
    {
        await new Login().GoToCredentials(page, results, selectedEnvironment);
        await new Login().ValidateLoginCorrect(page, results, selectedEnvironment, false);
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await page.Locator("a[href=\"/havanna/Home/AboutUs\"]").ClickAsync(); //Sobre havanna
            await page.Locator("div[class=\"box\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Footer Sobre Havanna", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Footer Sobre Havanna -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Footer Sobre Havanna", $"FAIL - {ex.Message}"));
        }
        try
        {
            //aviso de privacidade
            await page.Locator("a[href=\"/havanna/Home/Privacy\"]").ClickAsync();
            await page.Locator("article[class=\"post post-full\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            stopwatch.Stop();
            results.Add(new TestResult("Footer Aviso de Privacidade", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex) ///
        {
            Console.WriteLine($"Footer Aviso de Privacidade -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Footer Aviso de Privacidade", $"FAIL - {ex.Message}"));
        }
        try
        {
            //politica de segurança
            await page.Locator("a[href=\"/havanna/Home/SecurityPolicy\"]").ClickAsync();
            await page.Locator("article[class=\"post post-full\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Footer Política de Segurança", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Footer Política de Segurança -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Footer Política de Segurança", $"FAIL - {ex.Message}"));
        }
        try
        {
            // Formas de Entrega
            await page.Locator("a[href=\"/havanna/Home/DeliveryWays\"]").ClickAsync(); // Formas de Entrega
            await page.Locator("div[class=\"box\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Footer Formas de Entrega", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Footer Formas de Entrega -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Footer Formas de Entrega", $"FAIL - {ex.Message}"));
        }
        try
        {
            // Formas de Pagamento
            await page.Locator("a[href=\"/havanna/Home/PaymentMethods\"]").ClickAsync(); // Formas de Pagamento
            await page.Locator("div[class=\"box\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Footer Formas de Pagamento", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Footer Formas de Pagamento -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Footer Formas de Pagamento", $"FAIL - {ex.Message}"));
        }
        try
        {
            // Política de Troca
            await page.Locator("a[href=\"/havanna/Home/ReturnedItems\"]").ClickAsync(); // Política de Troca
            await page.Locator("div[class=\"box\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Footer Política de Troca", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Footer Política de Troca -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Footer Política de Troca", $"FAIL - {ex.Message}"));
        }
        try
        {
            // FAQ
            await page.Locator("a[href=\"/havanna/CustomerService/FAQ\"] >> text=FAQ").ClickAsync(); // FAQ
            await page.Locator("div[class=\"box\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Footer FAQ", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Footer FAQ -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Footer FAQ", $"FAIL - {ex.Message}"));
        }
        try
        {
            // Fazer Uma Solicitação
            await page.Locator("css=ul[class='footer-list footer-list-info text-center'] a[href=\"/havanna/CustomerService/OpenTicket\"]").ClickAsync(); // Fazer Uma Solicitação
            await page.Locator("div[class=\"box-header\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Footer Fazer Uma Solicitação", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Footer Fazer Uma Solicitação -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Footer Fazer Uma Solicitação", $"FAIL - {ex.Message}"));
        }
        try
        {
            // Historico de Solicitações
            await page.Locator("css=ul[class='footer-list footer-list-info text-center'] a[href=\"/havanna/CustomerService/ListTickets\"]").ClickAsync(); // Historico de Solicitações
            await page.Locator("div[class=\"box-header\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Footer Historico de Solicitações", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Footer Historico de Solicitações -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Footer Historico de Solicitações", $"FAIL - {ex.Message}"));
        }
    }

    public async Task ValidateLinksHeader(IPage page, List<TestResult> results, string selectedEnvironment)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            // Lista de Desejos
            await page.Locator("button[class=\"dropbtn havanna-icons\"]").HoverAsync();
            await page.Locator("a[id='btn-lista-desejos']").ClickAsync(); // Lista de Desejos
            await page.Locator("div[class=\"box\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Header Lista de Desejos", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Header Lista de Desejos -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Header Lista de Desejos", $"FAIL - {ex.Message}"));
        }
        try
        {
            // Carrinho de Compras
            await page.Locator("a[id='btn-carrinho']").ClickAsync(); // Carrinho de Compras
            await page.Locator("div[class=\"box-header\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Header Carrinho de Compras", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Header Carrinho de Compras -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Header Carrinho de Compras", $"FAIL - {ex.Message}"));
        }
    }

    public async Task ValidateLinksMyOrders(IPage page, List<TestResult> results, string selectedEnvironment)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            // Meus Pedidos e Detalhes
            await page.Locator("button[class=\"dropbtn havanna-icons\"]").HoverAsync(); // Hover em Meus Pedidos
            await page.Locator("id=btn-meus-pedidos").ClickAsync(); // Clique em Meus Pedidos
            await page.Locator("(//*[@class='col_view text-right']/a)[1]").ClickAsync();
            await page.Locator("text=Detalhes do seu pedido").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Tela Meus Pedidos e Detalhes", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Tela Meus Pedidos e Detalhes -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Tela Meus Pedidos e Detalhes", $"FAIL - {ex.Message}"));
        }
    }

    public async Task ValidateLinksMyData(IPage page, List<TestResult> results, string selectedEnvironment)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            // Meus Dados
            await page.Locator("button[class=\"dropbtn havanna-icons\"]").HoverAsync(); // Hover em Meus Dados
            await page.Locator("id=btn-meus-dados").ClickAsync(); // Clique em Meus Dados
            await page.Locator("css=input[id='firstNameTextBox']").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Tela Meus Dados", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Tela Meus Dados -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Tela Meus Dados", $"FAIL - {ex.Message}"));
        }

        try
        {
            // Tela de Endereços
            await page.Locator("css=i[class=\"fa fa-map-marker fa-2x\"]").ClickAsync(); // Clique no ícone de endereço
            await page.Locator("id=btnAdicionarEndereco").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            await page.Locator("id=btnAdicionarEndereco").ClickAsync();
            await new Login().LogOff(page);
            stopwatch.Stop();
            results.Add(new TestResult("Tela de Endereços", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Tela de Endereços -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Tela de Endereços", $"FAIL - {ex.Message}"));
        }
    }

    public async Task ValidateProductDetails(IPage page, List<TestResult> results, string selectedEnvironment)
    {
        var stopwatch = Stopwatch.StartNew();
        await new Orders(_baseConfig).SearchAvailableProduct(page, results, selectedEnvironment);
        await new Orders(_baseConfig).ClickBuy(page, results);

        try
        {
            //Valida se há uma imagem
            await page.Locator("div[class=\"product-images visible-md visible-lg\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("div[class=\"prices\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("div[id=\"shippingForm\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            //Valida CEP Inválido
            await page.Locator("input[id=\"postalCodeTextBox\"]").FillAsync("87053444");
            await Task.Delay(3000);
            await page.Locator("div[class=\"modal-header modal-error\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("div[class=\"modal-footer\"] button").ClickAsync();
            await page.Locator("input[id=\"postalCodeTextBox\"]").ClickAsync();
            await page.Keyboard.PressAsync("Control+A");
            await page.Keyboard.PressAsync("Backspace");

            //Valida CEP Válido
            await page.Locator("input[id=\"postalCodeTextBox\"]").FillAsync("87053220");
            await Task.Delay(5000);
            await page.Locator("//span[@class='shipping-postal-code'][contains(.,'87053-220')]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });


            // Valida botão Mais
            await page.Locator("button[title=\"Mais\"]").ClickAsync();
            await page.Locator("ul[class=\"dropdown-menu\"] a[title=\"Adicionar à Lista de Desejos\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("ul[class=\"dropdown-menu\"] a[title=\"Descrição\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("ul[class=\"dropdown-menu\"] a[title=\"Avaliações\"]").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("button[title=\"Mais\"]").ClickAsync();

            stopwatch.Stop();
            results.Add(new TestResult("Tela de Detalhes do Produto | CEP Válido e Inválido", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Tela de Detalhes do Produto | CEP Válido e Inválido -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Tela de Detalhes do Produto | CEP Válido e Inválido", $"FAIL - {ex.Message}"));
        }

    }
}









