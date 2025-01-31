using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class BaseConfig
{
    private readonly string _connectionString;

    public BaseConfig(IConfiguration configuration)
    {
        // Lê a string de conexão do arquivo appsettings.json
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private async Task<string> ExecuteScalarQuery(string query)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    var result = await command.ExecuteScalarAsync();
                    return result?.ToString(); // Retorna o valor ou null
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao executar consulta: {ex.Message}");
            return null;
        }
    }

    // Consulta específica para buscar o parDocumentId
    public async Task<string> SearchParDocumentId()
    {
        string queryStatusF = @"
            SELECT TOP 1 parDocumentID
            FROM tbParticipant 
            WHERE parStatus = 'F' 
            AND idCampaign = 'BRHAV' 
            AND parDocumentId IS NOT NULL 
            AND parDocumentId <> '' 
            AND MustChangePassword = 0
            AND parMobile  <> ''
            AND parGender  <> ''
            AND parBirthDate IS NOT NULL
            AND parEmail <> ''";

        return await ExecuteScalarQuery(queryStatusF);
    }

    // Consulta específica para buscar o produto
    public async Task<string> SearchProduct()
    {
        string querysSearchProduct = @"
            SELECT TOP 1 p.proProviderCode 
            FROM tbProduct p
            INNER join tbProdXCamp pc on pc.idProduct = p.idProduct 
            WHERE pc.IdCampaign ='BRHAV'
            AND p.proStockQuantityShop > 0 
            AND p.StockCurrent > 0";

        return await ExecuteScalarQuery(querysSearchProduct);
    }
}
