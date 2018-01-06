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
        #region " ### campos " 

        private ToDoViewModel _todoViewModel;

        #endregion

        #region "#### Construtor " 

        public CarregarAtividadeCommand(ToDoViewModel _viewModel)
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
            return _todoViewModel.ListDeAtividadeToDo == null;
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
            var client = HttpClientRequest.getClient();

            HttpResponseMessage response = client.GetAsync(EnderecosWebAPI._getByStatus+0).Result;
            Uri envioUri = response.Headers.Location;

            if (response.IsSuccessStatusCode)
            {
                var _atividadesTodo = response.Content.ReadAsAsync<IEnumerable<AtividadeToDo>>().Result;

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
                
                
                //Console.Out.WriteLine("teste");
                //Response.Redirect("Default?guid=" + Guid.NewGuid() + "&id=sucesso");
            }
            //else
            //Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString());

            //var clube = new ClubeDeFutebol();
            //clube.Nome = m_ViewModel.Nome;
            //clube.Tecnico = m_ViewModel.Tecnico;
            //clube.IDEstado = m_ViewModel.Estado;

            //var clubeRepository = new ClubeRepository(m_ViewModel);

            ////Valida se é uma edição ou inclusão de novo registro
            //if (m_ViewModel.IDClube == 0)
            //{
            //    clube.IdClube = m_ViewModel.ListClubesDeFutebol.Count + 1;
            //    clubeRepository.Insert(clube);
            //}
            //else
            //{
            //    clube.IdClube = m_ViewModel.IDClube;
            //    clubeRepository.Update(clube);
            //}

            //m_ViewModel.IDClube = 0;
            //m_ViewModel.Nome = string.Empty;
            //m_ViewModel.Tecnico = string.Empty;
            //m_ViewModel.Estado = null;
        }
        #endregion
    }
}
