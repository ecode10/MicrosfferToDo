using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicrosfferToDo.WebSite.Models
{
    public class AtividadesToDo
    {
        /// <summary>
        /// Id chave da atividade
        /// </summary>
        [Key]
        public Int64 IdTodo { get; set; }

        /// <summary>
        /// Nome da atividade
        /// </summary>
        public string NomeTodo { get; set; }

        /// <summary>
        /// Descricao da atividade
        /// </summary>
        public string DescricaoTodo { get; set; }

        /// <summary>
        /// Data da atividade
        /// </summary>
        public string DataTodo { get; set; }

        /// <summary>
        /// Horário da atividade
        /// </summary>
        public string HoraTodo { get; set; }

        /// <summary>
        /// Status da atividade
        /// </summary>
        public int CompletoTodo { get; set; }
    }
}