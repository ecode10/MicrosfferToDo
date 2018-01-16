namespace MicrosfferToDo.Library.Constantes
{
    /// <summary>
    /// Classe responsável pelo token e password do WebAPI
    /// Sem passar esses dados o Web API não vai funcionar
    /// Foi colocado uma segurança no Web API para que os métodos fossem acessados apenas com autorização
    /// Ao contrário, é retornado 'Não autorizado'.
    /// <author>
    /// Mauricio Junior
    /// </author>
    /// </summary>
    public static class TokenWebApi
    {
        public const string PublicToken = "561d1cc4-c7b5-431e-94a7-e0c2ed9a8d2c";
        public const string Pwd = "micr@$ffer.T@D@";
    }
}
