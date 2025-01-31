using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;

namespace AutomationQA.GenericActions;

internal class WaitManualAction 
{
    public async Task WaitForManualAction(IPage page)
    {
        await page.PauseAsync(); // Ativa o inspector do Playwright
        Console.Write("Execução pausada para análise do tester!");      
    }
}
