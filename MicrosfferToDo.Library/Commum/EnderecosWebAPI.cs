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
        public const string _enderecoBase = "http://localhost:50307/";
        public const string _post = "api/AtividadesToDo";
        public const string _get = "api/AtividadesToDo";
        public const string _delete = "api/AtividadesToDo/"; //{Id}
        public const string _put = "api/AtividadesToDo/"; //{Id}
        public const string _getByStatus = "api/AtividadesToDo/Status/"; //{Id} 0 ou 1
    }
}
