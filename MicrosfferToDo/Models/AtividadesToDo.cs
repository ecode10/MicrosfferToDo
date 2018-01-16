using System.ComponentModel.DataAnnotations;

namespace MicrosfferToDo.Models
{
    /// <summary>
    /// Classe que faz uma relação direta com as propriedades do banco de dados.
    /// <author>Mauricio Junior</author>
    /// </summary>
    public class AtividadesToDo
    {
        /// <summary>
        /// Id chave da atividade
        /// </summary>
        [Key]
        public long IdTodo { get; set; }

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