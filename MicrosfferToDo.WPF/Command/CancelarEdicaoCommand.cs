using MicrosfferToDo.WPF.ViewModel;
using System;
using System.Windows.Input;

namespace MicrosfferToDo.WPF.Command
{
    public class CancelarEdicaoCommand : ICommand
    {
        /// <summary>
        /// Campos da View Model
        /// </summary>
        public ToDoViewModel TodoViewModel { get; }


        /// <summary>
        /// Construtor da Classe
        /// </summary>
        /// <param name="viewModel"></param>
        public CancelarEdicaoCommand(ToDoViewModel viewModel)
        {
            TodoViewModel = viewModel;
        }
        

        
        /// <summary>
        /// Verifica se o campo nome está preenchido para habilitar
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(TodoViewModel.NomeTodo) || TodoViewModel.Atividade != null;
        }

        /// <summary>
        /// Ações que podem acontecer quando a ação muda
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Limpa os dados
        /// </summary>
        public void Execute(object parameter)
        {
            TodoViewModel.IdTodo = 0;
            TodoViewModel.NomeTodo = string.Empty;
            TodoViewModel.Atividade = null;

            TodoViewModel.BtnSalvar = Library.Constantes.Wpf.BotaoAdicionar;
            TodoViewModel.GridHabilitado = true;
        }
        
    }
}
