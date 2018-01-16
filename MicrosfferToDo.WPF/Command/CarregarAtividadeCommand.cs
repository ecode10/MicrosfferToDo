using MicrosfferToDo.Library.Util;
using MicrosfferToDo.WPF.Model;
using MicrosfferToDo.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using MicrosfferToDo.Library.Constantes;

namespace MicrosfferToDo.WPF.Command
{
    public class CarregarAtividadeCommand : ICommand
    {
        /// <summary>
        /// Propriedade privda contendo os campos da View
        /// </summary>
        public ToDoViewModel TodoViewModel { get; }


        /// <summary>
        /// Construtor da classe
        /// Atribui os dados para a propriedade privada
        /// </summary>
        /// <param name="viewModel"></param>
       public CarregarAtividadeCommand(ToDoViewModel viewModel)
        {
            TodoViewModel = viewModel;
        }
       
        
        /// <summary>
        /// Executa a limpeza da Lista
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return TodoViewModel.ListDeAtividadeToDo == null;
        }

        /// <summary>
        /// Ações que podem acontecer de acordo com alguma mudança
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Carrega a lista de dados vindos do Web Api
        /// </summary>
        public void Execute(object parameter)
        {
            //Chama a classe de cliente
            //Token e Password
            var client = HttpClientRequest.GetClient();

            //Pega os dados do Web Api baseado na consulta
            HttpResponseMessage response = client.GetAsync(EnderecosWebApi.GetByStatus+0).Result;

            //Se o retorno vier com sucesso, preenche a lista
            if (response.IsSuccessStatusCode)
            {
                //pega o resultado dos dados e retorna para a variável
                var atividadesTodo = response.Content.ReadAsAsync<IEnumerable<AtividadeToDo>>().Result;

                //Preenche a lista para retorno
                ObservableCollection<AtividadeToDo> list = TodoViewModel.ListDeAtividadeToDo; //new ObservableCollection<AtividadeToDo>(_atividadesTodo);
                list.Clear();
                foreach (var item in atividadesTodo)
                {
                    AtividadeToDo ativ = new AtividadeToDo
                    {
                        NomeTodo = item.NomeTodo,
                        IdTodo = item.IdTodo
                    };

                    list.Add(ativ);
                }

                TodoViewModel.ListDeAtividadeToDo = list;
            }
            
        }
        
    }
}
