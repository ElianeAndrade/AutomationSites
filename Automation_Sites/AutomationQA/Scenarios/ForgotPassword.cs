using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using System.Diagnostics;

namespace AutomationQA.Scenarios;

internal class ForgotPassword
{
    public async Task RecoverPassword(IPage page, List<TestResult> results, string selectedEnvironment)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await new Login().GoToCredentials(page, results, selectedEnvironment);
            Login setUserLogin = new Login();
            var (login, senha) = setUserLogin.SetUser(selectedEnvironment);  // Obtém o login e a senha

            var user = page.Locator("id=loginEmail");
            await user.FillAsync(login);
            await page.Locator("id=linkForgotPassword").ClickAsync();
            await page.Locator("id=emailForgot").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("id=emailForgot").FillAsync("10205210-teste@satelital.com.br");
            //await WaitManualAction.WaitForManualAction(page);
            await page.Locator("id=btnForgotPassword").ClickAsync();
            await page.Locator("div[class='modal-footer'] button[type='button']").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
            await page.Locator("div[class='modal-footer'] button[type='button']").ClickAsync();
            await page.Locator("id=loginEmail").WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });

            stopwatch.Stop();
            results.Add(new TestResult("Esqueci Senha", "PASS", executionTime: stopwatch.ElapsedMilliseconds));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Esqueci Senha -> FAIL: {ex.Message} \n");
            results.Add(new TestResult("Esqueci Senha", $"FAIL - {ex.Message}"));
        }
    }
}
