using MicrosfferToDo.Library.Commum;
using MicrosfferToDo.Library.Util;
using MicrosfferToDo.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

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
            var client = HttpClientRequest.getClient();

            HttpResponseMessage response = client.GetAsync(EnderecosWebAPI._getByStatus + numeroStatus).Result;
            Uri envioUri = response.Headers.Location;
            return response;
        }

        /// <summary>
        /// Método responsável pelo insert no web api via post
        /// Chama o projeto Web Api
        /// </summary>
        /// <param name="_atividades">AtividadesToDo</param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage InserirDadosWebApi(AtividadesToDo _atividades)
        {
            //preparando o web api com token e password
            var client = HttpClientRequest.getClient();

            HttpResponseMessage response = client.PostAsJsonAsync(EnderecosWebAPI._post, _atividades).Result;
            Uri envioUri = response.Headers.Location;
            return response;
        }

        /// <summary>
        /// Método que deleta os dados do Web Api
        /// </summary>
        /// <param name="_IdToDo">string</param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage DeletarDadosWebApi(string _IdToDo)
        {
            //preparando o web api com token e password
            var client = HttpClientRequest.getClient();

            HttpResponseMessage response = client.DeleteAsync(EnderecosWebAPI._delete + _IdToDo).Result;
            Uri envioUri = response.Headers.Location;
            return response;
        }

        /// <summary>
        /// Método que atualiza os dados do Web Api
        /// </summary>
        /// <param name="chave">string</param>
        /// <param name="_atividades">AtividadesToDo</param>
        /// <returns></returns>
        public static HttpResponseMessage AtualizarDadosWebApi(string chave, AtividadesToDo _atividades)
        {
            //preparando o web api com token e password
            var client = HttpClientRequest.getClient();

            HttpResponseMessage response = client.PutAsJsonAsync(EnderecosWebAPI._put + chave, _atividades).Result;
            Uri envioUri = response.Headers.Location;
            return response;
        }
    }
}