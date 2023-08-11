using AtendenteChatGPT.Models.Interface;
using Newtonsoft.Json;

namespace AtendenteChatGPT.Models;

public class JsonFileService<T> : IJsonFileService<T>
{
    private readonly string _arquivoJson = "Data/conversa.json";

    public List<T> LerDados()
    {
        using (var arquivo = File.OpenText(_arquivoJson))
        {
            var conteudo = arquivo.ReadToEnd();
            return JsonConvert.DeserializeObject<List<T>>(conteudo);
        }
    }

    public void GravarDados(List<T> dados)
    {
        var conteudo = JsonConvert.SerializeObject(dados, Formatting.Indented);
        File.WriteAllText(_arquivoJson, conteudo);
    }

}
