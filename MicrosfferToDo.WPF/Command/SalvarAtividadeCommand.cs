using MicrosfferToDo.Library.Util;
using MicrosfferToDo.WPF.Model;
using MicrosfferToDo.WPF.ViewModel;
using System;
using System.Net.Http;
using System.Windows.Input;
using MicrosfferToDo.Library.Constantes;

namespace MicrosfferToDo.WPF.Command
{
    public class SalvarAtividadeCommand : ICommand
    {
        /// <summary>
        /// Campos privados
        /// </summary>
        public ToDoViewModel ViewModel { get; }

        public SalvarAtividadeCommand(ToDoViewModel viewModel)
        {
            //atribui o que foi passado para a propriedade privada
            ViewModel = viewModel;
        }
        
        
        /// <summary>
        /// Método pode executar se a propriedade nome for preenchida
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(ViewModel.NomeTodo);
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
        /// Chama a Web API para salvar / editar os dados preenchidos
        /// Método utilizado para incluir ou editar
        /// </summary>
        public void Execute(object parameter)
        {
            //Verifica se Id foi é igual a 0 para Salvar
            if (ViewModel.IdTodo == 0)
            {
                //Chama o método para preparar para o Web API
                //Token
                //Password
                var client = HttpClientRequest.GetClient();

                //preenche os dados da classe
                var atividades = new AtividadeToDo()
                {
                    NomeTodo = ViewModel.NomeTodo,
                    CompletoTodo = 0
                };

                //Chama o Web API Post passando o endereço (constante) e a classe preenchida.
                HttpResponseMessage response = client.PostAsJsonAsync(EnderecosWebApi.Post, atividades).Result;

                //se for com sucesso carrega os dados
                if (response.IsSuccessStatusCode)
                {
                    CarregarAtividadeCommand carregaAtividadeCommand = new CarregarAtividadeCommand(ViewModel);
                    carregaAtividadeCommand.Execute(null);

                    CancelarEdicaoCommand cancelaEdicaoCommand = new CancelarEdicaoCommand(ViewModel);
                    cancelaEdicaoCommand.Execute(null);
                }
            }
            else // igual a 1 para editar
            {
                //chama a classe para atualizar
                AtualizarAtividadeCommand atualiza = new AtualizarAtividadeCommand(ViewModel);
                atualiza.Execute(null);

                CancelarEdicaoCommand cancelaEdicaoCommand = new CancelarEdicaoCommand(ViewModel);
                cancelaEdicaoCommand.Execute(null);
            }
        }
        
    }
}
