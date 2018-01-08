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
        /// <summary>
        /// Campos da View Model
        /// </summary>
        #region " ### Campos da ViewModel " 

        private ToDoViewModel _todoViewModel;

        #endregion

        /// <summary>
        /// Construtor da Classe
        /// </summary>
        /// <param name="_viewModel"></param>
        #region "#### Construtor " 

        public CancelarEdicaoCommand(ToDoViewModel _viewModel)
        {
            _todoViewModel = _viewModel;
        }
        #endregion

        /// <summary>
        /// Método da Interface ICommand
        /// </summary>
        #region ICommand Members

        /// <summary>
        /// Verifica se o campo nome está preenchido para habilitar
        /// </summary>
        public bool CanExecute(object parameter)
        {
            if (!string.IsNullOrEmpty(_todoViewModel.NomeTodo))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Ações que podem acontecer quando a ação muda
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Limpa os dados
        /// </summary>
        public void Execute(object parameter)
        {
            _todoViewModel.IdTodo = 0;
            _todoViewModel.NomeTodo = string.Empty;
        }
        #endregion
    }
}
