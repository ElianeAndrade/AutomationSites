using AutomationQA.GenericActions;

namespace AutomationQA.MenusEnvironments;
internal class MenuClass : Welcome
{
    public string ChooseClass()
    {
        var ScenarioOptions = new List<string>()
        {
            "Abrir Chamado", 
            "Cadastro", 
            "Login", 
            "Pedido",
            "Esqueci Senha", 
            "StatusF", 
            "Telas", 
            "Meus Dados", 
            "Executar Todos"

        };

        while (true)
        {
            Console.Clear();
            Presentation("CENÁRIOS PREVISTOS:");
            for (int i = 0; i < ScenarioOptions.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {ScenarioOptions[i]}");
            }

            Console.Write("\nInforme o cenário que deseja executar: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= ScenarioOptions.Count)
            {
                string selectedClass = ScenarioOptions[choice - 1];
                Console.Clear();
                return selectedClass;
            }
            Console.WriteLine("\nEscolha inválida. Pressione qualquer tecla para tentar novamente.");
            Console.ReadKey(); 
        }

    }



}