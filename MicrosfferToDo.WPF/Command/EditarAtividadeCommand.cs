using System;
using MicrosfferToDo.WPF.ViewModel;
using System.Windows.Input;

namespace MicrosfferToDo.WPF.Command
{
    public class EditarAtividadeCommand : ICommand
    {
        /// <summary>
        /// Propriedade privada da ViewModel
        /// </summary>
        public ToDoViewModel TodoViewModel { get; }

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="viewModel">ToDoViewModel</param>
        public EditarAtividadeCommand(ToDoViewModel viewModel)
        {
            //atribui os dados da View Model para a propriedade privada
            TodoViewModel = viewModel;
        }
        
        /// <summary>
        /// Método que pode ser executado se a propriedade for preenchida
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return TodoViewModel.Atividade != null;
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
        /// Atribui os dados para a View já preenchidos
        /// </summary>
        public void Execute(object parameter)
        {
            var atividade = TodoViewModel.Atividade;

            TodoViewModel.NomeTodo = atividade.NomeTodo;
            TodoViewModel.IdTodo = atividade.IdTodo;

            TodoViewModel.BtnSalvar = "Salvar alterações";
            TodoViewModel.GridHabilitado = false;
        }
    }
}
