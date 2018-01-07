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
        #region " ### campos " 

        private ToDoViewModel _todoViewModel;

        #endregion

        #region "#### Construtor " 

        public EditarAtividadeCommand(ToDoViewModel _viewModel)
        {
            _todoViewModel = _viewModel;
        }
        #endregion

        #region ICommand Members

        /// <summary>
        /// 
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return _todoViewModel.Atividade != null;
        }

        /// <summary>
        /// Actions to take when CanExecute() changes.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Inclui um novo clube ou altera um existente.
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
