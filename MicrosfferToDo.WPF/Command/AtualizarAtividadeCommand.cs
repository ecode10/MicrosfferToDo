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
    public class AtualizarAtividadeCommand : ICommand
    {
        /// <summary>
        /// Campos da View Model
        /// </summary>
        #region " ### campos " 

        private ToDoViewModel _todoViewModel;

        #endregion

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="_viewModel"></param>
        #region "#### Construtor " 

        ///<summary>
        /// Recebe os objetos da ViewModel e atribui para a propriedade privada da classe
        /// </summary>
        public AtualizarAtividadeCommand(ToDoViewModel _viewModel)
        {
            _todoViewModel = _viewModel;
        }
        #endregion

        ///<summary>
        /// Membros da interface ICommand
        /// </summary>
        #region ICommand Members

        /// <summary>
        /// Verifica a propriedade para habilitar o botão ou não
        /// </summary>
        public bool CanExecute(object parameter)
        {
            if (!string.IsNullOrEmpty(_todoViewModel.NomeTodo))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Ação que verifica se pode executar CanExecute(object).
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Executa a alteração dos dados de acordo com o que foi recebido da View
        /// Chama a Web API passando os parâmetros
        /// </summary>
        public void Execute(object parameter)
        {
            //Chama a classe Request
            //Dentro da classe tem o Token e a Senha
            var client = HttpClientRequest.getClient();

            //preenche os dados da classe para passar para a WebAPI
            var _atividades = new AtividadeToDo()
            {
                NomeTodo = _todoViewModel.NomeTodo,
                CompletoTodo = 0,
                IdTodo = _todoViewModel.IdTodo
            };

            //Chama a classe PUT passando a constante, id e os dados
            HttpResponseMessage response = client.PutAsJsonAsync(EnderecosWebAPI._put + _todoViewModel.IdTodo, _atividades).Result;
            Uri envioUri = response.Headers.Location;

            //Se a resposta do Web Api retornar com sucesso, carrega os dados novamente
            if (response.IsSuccessStatusCode)
            {
                CarregarAtividadeCommand _carregar = new CarregarAtividadeCommand(_todoViewModel);
                _carregar.Execute(null);
            }
        }
        #endregion
    }
}
