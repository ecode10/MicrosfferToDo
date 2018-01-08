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
    public class CarregarAtividadeCommand : ICommand
    {
        /// <summary>
        /// Propriedade privda contendo os campos da View
        /// </summary>
        #region " ### Campos da View " 

        private ToDoViewModel _todoViewModel;

        #endregion

        /// <summary>
        /// Construtor da classe
        /// Atribui os dados para a propriedade privada
        /// </summary>
        /// <param name="_viewModel"></param>
        #region "#### Construtor " 

        public CarregarAtividadeCommand(ToDoViewModel _viewModel)
        {
            _todoViewModel = _viewModel;
        }
        #endregion

        /// <summary>
        /// Membros da Interface Command
        /// </summary>
        #region ICommand Members

        /// <summary>
        /// Executa a limpeza da Lista
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return _todoViewModel.ListDeAtividadeToDo == null;
        }

        /// <summary>
        /// Ações que podem acontecer de acordo com alguma mudança
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Carrega a lista de dados vindos do Web Api
        /// </summary>
        public void Execute(object parameter)
        {
            //Chama a classe de cliente
            //Token e Password
            var client = HttpClientRequest.getClient();

            //Pega os dados do Web Api baseado na consulta
            HttpResponseMessage response = client.GetAsync(EnderecosWebAPI._getByStatus+0).Result;
            Uri envioUri = response.Headers.Location;

            //Se o retorno vier com sucesso, preenche a lista
            if (response.IsSuccessStatusCode)
            {
                //pega o resultado dos dados e retorna para a variável
                var _atividadesTodo = response.Content.ReadAsAsync<IEnumerable<AtividadeToDo>>().Result;

                //Preenche a lista para retorno
                ObservableCollection<AtividadeToDo> list = _todoViewModel.ListDeAtividadeToDo; //new ObservableCollection<AtividadeToDo>(_atividadesTodo);
                list.Clear();
                foreach (var item in _atividadesTodo)
                {
                    AtividadeToDo _ativ = new AtividadeToDo();
                    _ativ.NomeTodo = item.NomeTodo;
                    _ativ.IdTodo = item.IdTodo;

                    list.Add(_ativ);
                }

                _todoViewModel.ListDeAtividadeToDo = list;
            }
            
        }
        #endregion
    }
}
