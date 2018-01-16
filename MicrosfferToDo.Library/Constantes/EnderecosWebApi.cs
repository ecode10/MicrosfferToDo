namespace MicrosfferToDo.Library.Constantes
{
    /// <summary>
    /// Classe criada para ter os endereços do web api
    /// Compartilhada entre o projeto
    /// 
    /// <author>Mauricio Junior</author>
    /// </summary>
    public static class EnderecosWebApi
    {
        //constantes utilizadas nos outros projetos WebSite, Web API e WPF
        public const string EnderecoBase = "https://microsffertodo.azurewebsites.net/";//http://localhost:50307/ mudar o endereço base do Web API
        public const string Post = "api/AtividadesToDo";
        public const string Get = "api/AtividadesToDo";
        public const string Delete = "api/AtividadesToDo/"; //{Id}
        public const string Put = "api/AtividadesToDo/"; //{Id}
        public const string GetByStatus = "api/AtividadesToDo/Status/"; //{Id} 0 ou 1
    }
}
