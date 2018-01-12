using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosfferToDo.Library.Commum
{
    /// <summary>
    /// Classe criada para ter os endereços do web api
    /// Compartilhada entre o projeto
    /// 
    /// <author>Mauricio Junior</author>
    /// </summary>
    public static class EnderecosWebAPI
    {
        //constantes utilizadas nos outros projetos WebSite, Web API e WPF
        public const string _enderecoBase = "https://microsffertodo.azurewebsites.net/";//http://localhost:50307/ mudar o endereço base do Web API
        public const string _post = "api/AtividadesToDo";
        public const string _get = "api/AtividadesToDo";
        public const string _delete = "api/AtividadesToDo/"; //{Id}
        public const string _put = "api/AtividadesToDo/"; //{Id}
        public const string _getByStatus = "api/AtividadesToDo/Status/"; //{Id} 0 ou 1
    }
}
