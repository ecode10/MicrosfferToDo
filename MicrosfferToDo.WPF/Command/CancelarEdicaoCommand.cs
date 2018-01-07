using MicrosfferToDo.Library.Commum;
using MicrosfferToDo.Library.Util;
using MicrosfferToDo.WPF.Model;
using MicrosfferToDo.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MicrosfferToDo.WPF.Command
{
    public class CancelarEdicaoCommand : ICommand
    {

        #region " ### campos " 

        private ToDoViewModel _todoViewModel;

        #endregion

        #region "#### Construtor " 

        public CancelarEdicaoCommand(ToDoViewModel _viewModel)
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
            if (!string.IsNullOrEmpty(_todoViewModel.NomeTodo))
                return true;
            else
                return false;
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
            _todoViewModel.IdTodo = 0;
            _todoViewModel.NomeTodo = string.Empty;
        }
        #endregion
    }
}
