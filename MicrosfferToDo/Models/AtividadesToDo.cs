using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicrosfferToDo.Models
{
    /// <summary>
    /// Classe que faz uma relação direta com as propriedades do banco de dados.
    /// <author>Mauricio Junior</author>
    /// </summary>
    public class AtividadesToDo
    {
        [Key]
        public Int64 idTodo { get; set; }

        public string nomeTodo { get; set; }

        public string descricaoTodo { get; set; }

        public DateTime dataTodo { get; set; }

        public string horaTodo { get; set; }

        public int completoTodo { get; set; }
    }
}