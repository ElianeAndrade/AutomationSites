using System.Text;
namespace AutomationQA.ReportsTests;

internal class Report
{
    public static void GenerateHtmlReport(List<TestResult> results)
    {
        StringBuilder htmlContent = new StringBuilder();

        // Início do HTML
        htmlContent.AppendLine("<html><head><title>Relatório de Testes</title>");
        htmlContent.AppendLine("<link href='https://fonts.googleapis.com/css2?family=Roboto:wght@400;500;700&display=swap' rel='stylesheet'>");
        htmlContent.AppendLine("<style>");

        // Estilo geral da página
        htmlContent.AppendLine("body { background: #f7f9fb; font-family: 'Roboto', sans-serif; margin: 0; padding: 0; color: #333; }");
        htmlContent.AppendLine("h1 { text-align: center; color: #fff; font-size: 36px; padding: 20px 0; background-color: #6C7D8C; margin: 0; border-radius: 10px 10px 0 0; }");

        // Estilo da tabela
        htmlContent.AppendLine("table { width: 85%; margin: 30px auto; border-radius: 10px; border-collapse: collapse; box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1); background-color: #fff; }");
        htmlContent.AppendLine("th, td { border: 1px solid #d0d7df; padding: 12px 15px; text-align: left; font-size: 16px; transition: background-color 0.3s ease; }");
        htmlContent.AppendLine("th { background-color: #5a6f7a; color: #fff; font-weight: bold; text-transform: uppercase; border-radius: 10px 10px 0 0; text-align: center; }");
        htmlContent.AppendLine("td { background-color: #f9fafb; }");

        // Estilos para FAIL e PASS
        htmlContent.AppendLine(".fail { color: #e74c3c; background-color: #fbe1e1; text-align: center; border-radius: 5px; }");
        htmlContent.AppendLine(".pass { color: #2ecc71; background-color: #d4f8d4; font-weight: 600; text-align: center; border-radius: 5px; }");

        // Efeito de hover suave nas células
        htmlContent.AppendLine("td:hover { background-color: #f1f3f5; cursor: pointer; }");
        htmlContent.AppendLine("th:hover { background-color: #4c5a63; }");

        // Estilo da coluna de tempo
        htmlContent.AppendLine(".time { text-align: center; background-color: #f1f1f1; font-weight: normal; }");

        htmlContent.AppendLine("</style></head><body>");

        // Título da página
        htmlContent.AppendLine("<h1>Relatório dos Testes - SiteA</h1>");

        // Tabela
        htmlContent.AppendLine("<table>");
        htmlContent.AppendLine("<tr><th>Nome do Cenário</th><th>Status</th><th>Tempo</th></tr>");

        double totalExecutionTeste = 0;

        foreach (var result in results)
        {
            htmlContent.AppendLine("<tr>");
            htmlContent.AppendLine($"<td>{result.TestName}</td>");

            // Aplica as classes para FAIL e PASS
            if (result.Status.Contains("FAIL"))
            {
                htmlContent.AppendLine($"<td class='fail'>{result.Status}</td>");
            }
            else
            {
                htmlContent.AppendLine($"<td class='pass'>{result.Status}</td>");
            }

            // Converte o tempo de execução em minutos e segundos
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(result.ExecutionTime);
            string formattedTime = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            htmlContent.AppendLine($"<td class='time'>{formattedTime}</td>");
            htmlContent.AppendLine("</tr>");

            totalExecutionTeste += result.ExecutionTime; // Acumula o tempo total
        }

        // Adiciona a linha de somatório de tempo no final da tabela
        htmlContent.AppendLine("<tr>");
        // Coloca o "Tempo Total de Execução dos Testes" na primeira coluna, mesclando as duas primeiras colunas
        htmlContent.AppendLine("<td colspan='2' style='text-align: center; font-weight: bold; background-color: #f0f0f0; text-transform: uppercase; '>Tempo Total de Execução</td>");
        // Calcula o tempo total
        TimeSpan totalTimeSpan = TimeSpan.FromMilliseconds(totalExecutionTeste);
        string totalFormattedTime = $"{totalTimeSpan.Minutes:D2}:{totalTimeSpan.Seconds:D2}";
        // Coloca o tempo total na terceira coluna (time)
        htmlContent.AppendLine($"<td class='time' style='background-color: #f0f0f0; font-weight: bold;'>{totalFormattedTime}</td>");
        htmlContent.AppendLine("</tr>");

        htmlContent.AppendLine("</table>");
        htmlContent.AppendLine("</body></html>");

        // Salva o arquivo HTML
        File.WriteAllText("RelatorioTestes.html", htmlContent.ToString());
    }
}


