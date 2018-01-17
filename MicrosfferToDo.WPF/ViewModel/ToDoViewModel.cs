using MicrosfferToDo.WPF.Command;
using MicrosfferToDo.WPF.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MicrosfferToDo.WPF.ViewModel
{
    /// <summary>
    /// Classe View Model da MVVP
    /// Comunica com a Model
    /// <athor>Mauricio Junior</athor>
    /// </summary>
    public class ToDoViewModel : INotifyPropertyChanged
    {
       
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ToDoViewModel()
        {
            //Chama o método que inicia
            Initialize();
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

     
        
        //propriedades utilizadas na view
        /// <summary>
        /// Propriedade responsável pela lista de atividades
        /// </summary>
        private ObservableCollection<AtividadeToDo> _listDeAtividadeToDo;

        public ObservableCollection<AtividadeToDo> ListDeAtividadeToDo
        {
            get => _listDeAtividadeToDo;

            set
            {
                _listDeAtividadeToDo = value;

                NotifyPropertyChanged("ListDeAtividadeToDo");

            }
        }

        /// <summary>
        /// Propriedade utilizada quando é feito uma seleção
        /// Por exemplo: caso usuário selecione uma atividade, essa propriedade será preenchida com todos os dados.
        /// </summary>
        private AtividadeToDo _atividade;

        public AtividadeToDo Atividade
        {
            get => _atividade;
            set
            {

                if (value != _atividade)
                {
                    _atividade = value;

                    NotifyPropertyChanged("Atividade");
                }
            }
        }

        /// <summary>
        /// Propriedade: nome da propriedade principal
        /// É o título da atividade
        /// </summary>
        private string _nomeTodo;

        public string NomeTodo
        {
            get => _nomeTodo;
            set
            {
                if (value != _nomeTodo)
                {
                    _nomeTodo = value;
                    NotifyPropertyChanged("NomeTodo");
                }
            }
        }

        /// <summary>
        /// Id da atividade
        /// Mesmo nome da propriedade do banco de dados
        /// </summary>
        private Int64 _idTodo;
        public Int64 IdTodo
        {
            get => _idTodo;
            set
            {
                if (value != _idTodo)
                {
                    _idTodo = value;
                    NotifyPropertyChanged("IdTodo");
                }
            }
        }

        /// <summary>
        /// Propriedade status
        /// Completou 1
        /// Não completou 0
        /// </summary>
        private int _completoTodo;

        public int CompletoTodo
        {
            get => _completoTodo;
            set
            {
                if (value != _completoTodo)
                {
                    _completoTodo = value;
                    NotifyPropertyChanged("CompletoTodo");
                }
            }
        }

        private string _btnSalvar;
        public string BtnSalvar
        {
            get => _btnSalvar;
            set
            {
                if (value != _btnSalvar)
                {
                    _btnSalvar = value;
                    NotifyPropertyChanged("BtnSalvar");
                }
            }
        }

        private bool _gridHabilitado;
        public bool GridHabilitado
        {
            get => _gridHabilitado;
            set
            {
                if (value != _gridHabilitado)
                {
                    _gridHabilitado = value;
                    NotifyPropertyChanged("GridHabilitado");
                }
            }
        }


        ///<summary>
        /// Salva as atividades
        /// Classe com o nome igual dentro da pasta Command
        ///</summary>
        public ICommand SalvarAtividadeCommand { get; set; }

        /// <summary>
        /// Deleta as atividades
        /// Classe com o nome igual dentro da pasta Command
        /// </summary>
        public ICommand DeletarAtividadeCommand { get; set; }

        /// <summary>
        /// Carrega as atividades do Grid
        /// Classe com o nome igual dentro da pasta Command
        /// </summary>
        public ICommand CarregarAtividadeCommand { get; set; }

        /// <summary>
        /// Edita as atividades
        /// Classe com o nome igual dentro da pasta Command
        /// </summary>
        public ICommand EditarAtividadeCommand { get; set; }

        /// <summary>
        /// Cancela a edição das atividades
        /// Classe com o nome igual dentro da pasta Command
        /// </summary>
        public ICommand CancelarEdicaoCommand { get; set; }
        

        
        /// <summary>
        /// Método que inicia propriedades e comandos
        /// </summary>
        private void Initialize()
        {
            //Zera a lista
            _listDeAtividadeToDo = null;
            _listDeAtividadeToDo = new ObservableCollection<AtividadeToDo>();

            //Inicia os comandos
            SalvarAtividadeCommand = new SalvarAtividadeCommand(this);
            CarregarAtividadeCommand = new CarregarAtividadeCommand(this);
            DeletarAtividadeCommand = new DeletarAtividadeCommand(this);
            EditarAtividadeCommand = new EditarAtividadeCommand(this);
            CancelarEdicaoCommand = new CancelarEdicaoCommand(this);

            //carrega as atividades
            CarregarAtividadeCommand.Execute(null);

            //
            BtnSalvar = Library.Constantes.Wpf.BotaoAdicionar;
            GridHabilitado = true;
        }
        
    }
}
