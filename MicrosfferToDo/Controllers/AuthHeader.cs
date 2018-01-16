using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Http;
using MicrosfferToDo.Library.Constantes;

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
        public HttpStatusCode AutorizarToken(HttpRequestHeaders req)
        {
            var headers = req; //this.Request.Headers;
            string token = string.Empty;
            string pwd = string.Empty;

            //verifica se contem as duas tags no header para atribuir os valores
            if (headers.Contains("token") && headers.Contains("pwd"))
            {
                token = headers.GetValues("token").First();
                pwd = headers.GetValues("pwd").First();
            }
            
            try
            {
                //verifica se os dados atribuídos são iguais ao da classe
                if (token.Equals(TokenWebApi.PublicToken) &&
                    pwd.Equals(TokenWebApi.Pwd))
                    return HttpStatusCode.Accepted;
                return HttpStatusCode.Unauthorized;
            }
            catch
            {
                return HttpStatusCode.Unauthorized;
            }
        }
    }
}