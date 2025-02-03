# Introdução
Este projeto foi desenvolvido para validar de forma automatizada as campanhas que possuem site de compras em geral (markeplace e etc.)

# O que é a Playwright?
Desenvolvido pela Microsoft, é uma biblioteca de automação de testes que pode ser usada com .NET para realizar testes de ponta a ponta em navegadores. 
Ele é uma alternativa moderna e oferece um ótimo desempenho, com recursos avançados para automação em aplicações web.

# Por onde começar?
Para que o projeto funcione adequadamente na sua máquina será necessário (além de clonar e rodar o repositório):
1.  Instalar o Visual Studio 2022 (caso esteja utilizando o VSCode, rodas os testes com o seguinte comando: dotnet nomeDoTeste).	
2.  Instalação do .net 8.0 (comando para verificar se já possui o .net: dotnet --version) 
3.  Instalar Playwright como uma ferramenta global na sua máquina. (comando: dotnet tool install --global Microsoft.Playwright.CLI).
4.  Instalar os navegadores que o Playwright suporta (comando: playwright install)
5.  Instale a biblioteca no projeto (comando: dotnet add package Microsoft.Playwright)
6.  Instale as bibliotecas de Dados Fakers (comando: dotnet add package Bogus | dotnet add package Faker.Net )
7.  Instale SQL Client para se conectar ao banco de dados (comando: dotnet add package Microsoft.Data.SqlClient)

 

# Como rodar os testes?
1. Selecione a pasta da campanha, abra o projeto .Sln e clique em iniciar projeto, Visual Studio. 
2. Defina o ambiente (ambiente de Teste ou Ammbiente de Release) e o cenário que deseja executar.
3. Para verificar o relatório de Testes, selecione o arquivo Relatório de Testes, dentro da pasta de cada projeto:  /bin/Debug/net8.0/RelatorioTestes.html

# Documentação
https://playwright.dev/docs/intro


# Para mais informações ou ajuda
Entrar em contato com os QAs:
emariaandrade@gmail.com

# Contribuição
Todas as contribuições para este projeto são bem-vindas. Caso tenha dúvidas ou sugestões, por favor, entre em contato.
