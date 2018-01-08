using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosfferToDo.Library.Commum
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
    public static class TokenWebAPI
    {
        public const string _publicToken = "561d1cc4-c7b5-431e-94a7-e0c2ed9a8d2c";
        public const string _pwd = "micr@$ffer.T@D@";
    }
}
