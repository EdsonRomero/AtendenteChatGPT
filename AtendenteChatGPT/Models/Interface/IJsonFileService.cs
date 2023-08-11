namespace AtendenteChatGPT.Models.Interface;

public interface IJsonFileService<T>
{
    List<T> LerDados();
    void GravarDados(List<T> dados);

}
