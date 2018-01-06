using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace MicrosfferToDo.Controllers
{
    /// <summary>
    /// Classe responsável pela autenticação do token do sistema
    /// </summary>
    public class AuthHeader : ApiController
    {
        /// <summary>
        /// Método que autentica o token do sistema.
        /// </summary>
        /// <param name="req">HttpRequestHeaders</param>
        /// <returns>HttpStatusCode</returns>
        public HttpStatusCode autorizarToken(HttpRequestHeaders req)
        {
            System.Net.Http.Headers.HttpRequestHeaders headers = req; //this.Request.Headers;
            string _token = string.Empty;
            string _pwd = string.Empty;

            if (headers.Contains("token") && headers.Contains("pwd"))
            {
                _token = headers.GetValues("token").First();
                _pwd = headers.GetValues("pwd").First();
            }
            
            try
            {
                if (_token.Equals(MicrosfferToDo.Library.Commum.TokenWebAPI._publicToken) &&
                    _pwd.Equals(MicrosfferToDo.Library.Commum.TokenWebAPI._pwd))
                    return HttpStatusCode.Accepted;
                else
                    return HttpStatusCode.Unauthorized;
            }
            catch
            {
                return HttpStatusCode.Unauthorized;
            }
        }
    }
}