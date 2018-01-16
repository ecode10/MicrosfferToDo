using MicrosfferToDo.Library.Common;
using System;
using System.Net.Http;
using MicrosfferToDo.Library.Constantes;

namespace MicrosfferToDo.Library.Util
{
    /// <summary>
    /// Classe responsável por voltar a variavel de client HttpClient
    /// <author>
    /// Mauricio Junior
    /// </author>
    /// </summary>
    public static class HttpClientRequest
    {
        /// <summary>
        /// Método que busca o tipo do cliente com o endereço base do web api
        /// </summary>
        /// <returns>HttpClient</returns>
        public static HttpClient GetClient()
        {
            //chama a classe HTTP Client
            HttpClient client = new HttpClient();

            //Atribui o endereço base
            client.BaseAddress = new Uri(EnderecosWebApi.EnderecoBase);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //Passa como parâmetro o token e senha no header
            client.DefaultRequestHeaders.Add("token", TokenWebApi.PublicToken);
            client.DefaultRequestHeaders.Add("pwd", TokenWebApi.Pwd);

            return client;
        }
    }
}
