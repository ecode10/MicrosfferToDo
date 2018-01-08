using System;
using System.Collections.Generic;
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
    public class EditarAtividadeCommand : ICommand
    {
        /// <summary>
        /// Propriedade privada da ViewModel
        /// </summary>
        #region " ### Campos " 

        private ToDoViewModel _todoViewModel;

        #endregion

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="_viewModel">ToDoViewModel</param>
        #region "#### Construtor " 

        public EditarAtividadeCommand(ToDoViewModel _viewModel)
        {
            //atribui os dados da View Model para a propriedade privada
            _todoViewModel = _viewModel;
        }
        #endregion

        /// <summary>
        /// Métodos membros da interface ICommand
        /// </summary>
        #region ICommand Members

        /// <summary>
        /// Método que pode ser executado se a propriedade for preenchida
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return _todoViewModel.Atividade != null;
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
        /// Atribui os dados para a View já preenchidos
        /// </summary>
        public void Execute(object parameter)
        {
            var _atividade = _todoViewModel.Atividade;

            _todoViewModel.NomeTodo = _atividade.NomeTodo;
            _todoViewModel.IdTodo = _atividade.IdTodo;
        }
        #endregion
    }
}
