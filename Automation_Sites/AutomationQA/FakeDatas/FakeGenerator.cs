namespace AutomationQA.FakeDatas
{

    public class FakeDataGenerator
    {
        // Gerador de dados de usuário de forma dinâmica
        public static Dictionary<string, string> GenerateDataFalse(string registrationType)
        {
            Bogus.Faker faker = new Bogus.Faker("pt_BR");

            // Método para gerar dados comuns
            Dictionary<string, string> GenerateCommonData()
            {
                return new Dictionary<string, string>
                {
                    ["nome"] = faker.Name.FirstName(),
                    ["sobrenome"] = faker.Name.LastName(),
                    ["email"] = faker.Internet.Email(),
                    ["rua"] = faker.Address.StreetName(),
                    ["logradouro"] = faker.Address.BuildingNumber(),
                    ["comp"] = faker.Address.StreetName(),
                    ["bairro"] = faker.Address.StreetName(),
                    ["ref"] = faker.Address.City(),
                    ["desc"] = faker.Company.CompanyName(),
                    ["senha"] = "Test123!",
                    ["celular"] = "44995656320",
                    ["cep"] = "13326120",
                };
            }

            // Método para gerar dados de CPF
            Dictionary<string, string> GenerateCpfData()
            {
                var data = GenerateCommonData();
                data["documento"] = GenerateCpf();
                data["nascimento"] = faker.Date.Past(25, DateTime.Now.AddYears(-18)).ToString("ddMMyyyy");
                return data;
            }

            // Método para gerar dados de CNPJ
            Dictionary<string, string> GenerateCnpjData()
            {
                var data = GenerateCommonData();
                data["documento"] = GenerateCnpj();
                data["nome"] = faker.Company.CompanyName();
                return data;
            }

            // Método para gerar dados de EMAIL
            Dictionary<string, string> GenerateEmailData()
            {
                var data = GenerateCommonData();
                data["documento"] = GenerateCpf();
                data["nascimento"] = faker.Date.Past(23, DateTime.Now.AddYears(-20)).ToString("ddMMyyyy");
                return data;
            }

            // Seleção do tipo de registro e geração dos dados
            return registrationType switch
            {
                "CPF" => GenerateCpfData(),
                "CNPJ" => GenerateCnpjData(),
                "EMAIL" => GenerateEmailData(),
                _ => throw new ArgumentException("Tipo de registro inválido!")
            };
        }



        private static string GenerateCpf()
        {
            Random random = new Random();
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string cpf = random.Next(100000000, 999999999).ToString();
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            cpf += resto;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            cpf += resto;

            return cpf;
        }

        // Método customizado para gerar CNPJ válido
        private static string GenerateCnpj()
        {
            Random random = new Random();
            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string cnpj = random.Next(10000000, 99999999).ToString() + "0001";
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            cnpj += resto;

            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            cnpj += resto;

            return cnpj;
        }
    }
}




