using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization; 
using System.IO;

public class RendimentoServico
{  
    public Task<double> ObterRendimento(string dataFinal)
{
    return Task.Run(() =>
    {
        try
        {            
            string json = File.ReadAllText("rendimentopoupanca.json"); 
            
            JArray arrayResponse = JArray.Parse(json);
            
            JObject? dataObject = arrayResponse.FirstOrDefault(item =>
            {
                var datafim = (string?)item["datafim"];
                if (datafim != null)
                {                    
                    return datafim == dataFinal;
                }
                return false;
            }) as JObject;
           
            if (dataObject != null)
            {
                string? valorString = (string?)dataObject["valor"];

                
                if (valorString != null)
                {                    
                    if (double.TryParse(valorString, out double valor))
                    {
                        return valor;
                    }
                }
            }
        }
        catch (IOException ex)
        {            
            Console.WriteLine($"Erro de IO: {ex.Message}");
        }
        catch (Exception ex)
        {           
            Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
        }
        
        return 0;
    });
}
}

