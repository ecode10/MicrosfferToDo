using MicrosfferToDo.Library.Commum;
using MicrosfferToDo.Library.Util;
using MicrosfferToDo.WPF.Model;
using MicrosfferToDo.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MicrosfferToDo.WPF.Command
{
    public class DeletarAtividadeCommand : ICommand
    {

        /// <summary>
        /// Propriedade da ViewModel
        /// </summary>
        #region " ### Campos " 

        private ToDoViewModel _todoViewModel;

        #endregion

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="_viewModel"></param>
        #region "#### Construtor " 

        public DeletarAtividadeCommand(ToDoViewModel _viewModel)
        {
            _todoViewModel = _viewModel;
        }
        #endregion

        /// <summary>
        /// Métodos da Interface ICommand
        /// </summary>
        #region ICommand Members

        /// <summary>
        /// Verifica a execução
        /// Caso o nome da estiver preenchido
        /// </summary>
        public bool CanExecute(object parameter)
        {
            if (!string.IsNullOrEmpty(_todoViewModel.NomeTodo))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Ações que podem acontecer de acordo com as mudanças.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Executa
        /// Deleta os dados do Web API
        /// </summary>
        public void Execute(object parameter)
        {
            //Chama o método
            //Token e Password
            var client = HttpClientRequest.getClient();

            //Chama o método de delete do Web API passando o Id selecionado
            HttpResponseMessage response = client.DeleteAsync(EnderecosWebAPI._delete+ _todoViewModel.Atividade.IdTodo).Result;
            Uri envioUri = response.Headers.Location;

            //No caso de sucesso dos dados, carrega os dados novamente da lista
            //Limpa a lista
            if (response.IsSuccessStatusCode)
            {
                CarregarAtividadeCommand _carr = new CarregarAtividadeCommand(_todoViewModel);
                _carr.Execute(null);

                _todoViewModel.IdTodo = 0;
                _todoViewModel.NomeTodo = string.Empty;
            }
        }
        #endregion
    }
}
