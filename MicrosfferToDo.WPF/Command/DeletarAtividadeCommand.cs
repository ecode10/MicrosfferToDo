using MicrosfferToDo.Library.Util;
using MicrosfferToDo.WPF.ViewModel;
using System;
using System.Net.Http;
using System.Windows.Input;
using MicrosfferToDo.Library.Constantes;

namespace MicrosfferToDo.WPF.Command
{
    public class DeletarAtividadeCommand : ICommand
    {
        /// <summary>
        /// Propriedade da ViewModel
        /// </summary>
        public ToDoViewModel TodoViewModel { get; }

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="viewModel"></param>
       public DeletarAtividadeCommand(ToDoViewModel viewModel)
        {
            TodoViewModel = viewModel;
        }
        

       
        /// <summary>
        /// Verifica a execução
        /// Caso o nome da estiver preenchido
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(TodoViewModel.NomeTodo) || TodoViewModel.Atividade!=null;
        }

        /// <summary>
        /// Ações que podem acontecer de acordo com as mudanças.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Executa
        /// Deleta os dados do Web API
        /// </summary>
        public void Execute(object parameter)
        {
            //Chama o método
            //Token e Password
            var client = HttpClientRequest.GetClient();

            //Chama o método de delete do Web API passando o Id selecionado
            HttpResponseMessage response = client.DeleteAsync(EnderecosWebApi.Delete+ TodoViewModel.Atividade.IdTodo).Result;

            //No caso de sucesso dos dados, carrega os dados novamente da lista
            //Limpa a lista
            if (response.IsSuccessStatusCode)
            {
                CarregarAtividadeCommand carregaAtividadeCommand = new CarregarAtividadeCommand(TodoViewModel);
                carregaAtividadeCommand.Execute(null);

                CancelarEdicaoCommand cancelaEdicaoCommand = new CancelarEdicaoCommand(TodoViewModel);
                cancelaEdicaoCommand.Execute(null);
            }
        }
    }
}
