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
    public class SalvarAtividadeCommand : ICommand
    {

        #region " ### campos " 

        private ToDoViewModel _todoViewModel;

        #endregion

        #region "#### Construtor " 

        public SalvarAtividadeCommand(ToDoViewModel _viewModel)
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
            if (_todoViewModel.IdTodo == 0)
            {
                var client = HttpClientRequest.getClient();

                //preenche os dados
                var _atividades = new AtividadeToDo()
                {
                    NomeTodo = _todoViewModel.NomeTodo,
                    CompletoTodo = 0
                };

                HttpResponseMessage response = client.PostAsJsonAsync(EnderecosWebAPI._post, _atividades).Result;
                Uri envioUri = response.Headers.Location;

                if (response.IsSuccessStatusCode)
                {
                    CarregarAtividadeCommand _carr = new CarregarAtividadeCommand(_todoViewModel);
                    _carr.Execute(null);
                }
            }
            else
            {
                AtualizarAtividadeCommand _atualiza = new AtualizarAtividadeCommand(_todoViewModel);
                _atualiza.Execute(null);
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
