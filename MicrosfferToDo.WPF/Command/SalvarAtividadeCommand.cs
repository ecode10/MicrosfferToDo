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
        /// <summary>
        /// Campos privados
        /// </summary>
        #region " ### Campos " 

        private ToDoViewModel _todoViewModel;

        #endregion

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="_viewModel"></param>
        #region "#### Construtor " 

        public SalvarAtividadeCommand(ToDoViewModel _viewModel)
        {
            //atribui o que foi passado para a propriedade privada
            _todoViewModel = _viewModel;
        }
        #endregion

        /// <summary>
        /// Métodos membros da Interface ICommand
        /// </summary>
        #region ICommand Members

        /// <summary>
        /// Método pode executar se a propriedade nome for preenchida
        /// </summary>
        public bool CanExecute(object parameter)
        {
            if (!string.IsNullOrEmpty(_todoViewModel.NomeTodo))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Ações que podem acontecer de acordo com as mudanças.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Executa
        /// Chama a Web API para salvar / editar os dados preenchidos
        /// Método utilizado para incluir ou editar
        /// </summary>
        public void Execute(object parameter)
        {
            //Verifica se Id foi é igual a 0 para Salvar
            if (_todoViewModel.IdTodo == 0)
            {
                //Chama o método para preparar para o Web API
                //Token
                //Password
                var client = HttpClientRequest.getClient();

                //preenche os dados da classe
                var _atividades = new AtividadeToDo()
                {
                    NomeTodo = _todoViewModel.NomeTodo,
                    CompletoTodo = 0
                };

                //Chama o Web API Post passando o endereço (constante) e a classe preenchida.
                HttpResponseMessage response = client.PostAsJsonAsync(EnderecosWebAPI._post, _atividades).Result;
                Uri envioUri = response.Headers.Location;

                //se for com sucesso carrega os dados
                if (response.IsSuccessStatusCode)
                {
                    CarregarAtividadeCommand _carr = new CarregarAtividadeCommand(_todoViewModel);
                    _carr.Execute(null);
                }
            }
            else // igual a 1 para editar
            {
                //chama a classe para atualizar
                AtualizarAtividadeCommand _atualiza = new AtualizarAtividadeCommand(_todoViewModel);
                _atualiza.Execute(null);
            }
        }
        #endregion
    }
}
