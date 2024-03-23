using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization; 
using System.IO;

public class RendimentoServico
{
    // Restante do código...

    public Task<double> ObterRendimento(string dataFinal)
{
    return Task.Run(() =>
    {
        try
        {
            // Carregar o conteúdo do arquivo JSON
            string json = File.ReadAllText("rendimentopoupanca.json"); // Substitua "nome_do_arquivo.json" pelo nome real do seu arquivo

            // Analisar o JSON
            JArray arrayResponse = JArray.Parse(json);

            // Procure pelo objeto que corresponde à data final desejada
            JObject? dataObject = arrayResponse.FirstOrDefault(item =>
            {
                var datafim = (string?)item["datafim"];
                if (datafim != null)
                {
                    // Compare as strings de data fornecidas diretamente
                    return datafim == dataFinal;
                }
                return false;
            }) as JObject;

            // Se o objeto for encontrado, obtenha o valor da chave "valor"
            if (dataObject != null)
            {
                string? valorString = (string?)dataObject["valor"];

                // Verifique se o valorString não é nulo antes de fazer a conversão
                if (valorString != null)
                {
                    // Converte o valor para double
                    if (double.TryParse(valorString, out double valor))
                    {
                        return valor;
                    }
                }
            }
        }
        catch (IOException ex)
        {
            // Trate exceções de IO (por exemplo, arquivo não encontrado)
            Console.WriteLine($"Erro de IO: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Trate outras exceções não previstas
            Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
        }

        // Se houver um erro ou o valor não puder ser obtido, retorne 0
        return 0;
    });
}
}

