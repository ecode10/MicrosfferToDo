using MicrosfferToDo.Library.Util;
using MicrosfferToDo.WebSite.Models;
using System;
using System.Net.Http;
using MicrosfferToDo.Library.Constantes;

namespace MicrosfferToDo.WebSite.Controllers
{
    /// <summary>
    /// Classe Controller de Web API
    /// Chama o Web Api passando os dados necessários
    /// O Web Api é um projeto a parte
    /// 
    /// Caso queira checar o Web Api, favor olhar no projeto MicrosoftToDo.WebAPI
    /// <author>Mauricio Junior</author>
    /// </summary>
    public class WebApiController
    {
        /// <summary>
        /// Método que consulta o Web Api baseado no status informado
        /// </summary>
        /// <param name="numeroStatus">int</param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage ConsultaWebApiByStatus(int numeroStatus)
        {
            //preparando o web api com token e password
            var client = HttpClientRequest.GetClient();

            HttpResponseMessage response = client.GetAsync(EnderecosWebApi.GetByStatus + numeroStatus).Result;
            return response;
        }

        /// <summary>
        /// Método responsável pelo insert no web api via post
        /// Chama o projeto Web Api
        /// </summary>
        /// <param name="atividades">AtividadesToDo</param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage InserirDadosWebApi(AtividadesToDo atividades)
        {
            //preparando o web api com token e password
            var client = HttpClientRequest.GetClient();

            HttpResponseMessage response = client.PostAsJsonAsync(EnderecosWebApi.Post, atividades).Result;
            return response;
        }

        /// <summary>
        /// Método que deleta os dados do Web Api
        /// </summary>
        /// <param name="idToDo">string</param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage DeletarDadosWebApi(string idToDo)
        {
            //preparando o web api com token e password
            var client = HttpClientRequest.GetClient();

            HttpResponseMessage response = client.DeleteAsync(EnderecosWebApi.Delete + idToDo).Result;
            return response;
        }

        /// <summary>
        /// Método que atualiza os dados do Web Api
        /// </summary>
        /// <param name="chave">string</param>
        /// <param name="atividades">AtividadesToDo</param>
        /// <returns></returns>
        public static HttpResponseMessage AtualizarDadosWebApi(string chave, AtividadesToDo atividades)
        {
            //preparando o web api com token e password
            var client = HttpClientRequest.GetClient();

            HttpResponseMessage response = client.PutAsJsonAsync(EnderecosWebApi.Put + chave, atividades).Result;
            return response;
        }
    }
}