using MicrosfferToDo.WPF.Command;
using MicrosfferToDo.WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <summary>
        /// Propriedade Notify que vincula a tela do MVVM
        /// </summary>
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        /// <summary>
        /// Propriedades utilizadas na View Model
        /// </summary>
        #region "### Propriedades "

        //propriedades utilizadas na view
        /// <summary>
        /// Propriedade responsável pela lista de atividades
        /// </summary>
        private ObservableCollection<AtividadeToDo> _listDeAtividadeToDo;

        public ObservableCollection<AtividadeToDo> ListDeAtividadeToDo
        {
            get { return _listDeAtividadeToDo; }

            set
            {
                _listDeAtividadeToDo = value;

                ///Mesmo nome utilizado no XAML
                this.NotifyPropertyChanged("ListDeAtividadeToDo");

            }
        }

        /// <summary>
        /// Propriedade utilizada quando é feito uma seleção
        /// Por exemplo: caso usuário selecione uma atividade, essa propriedade será preenchida com todos os dados.
        /// </summary>
        private AtividadeToDo _atividade;

        public AtividadeToDo Atividade
        {
            get { return _atividade; }
            set
            {

                if (value != _atividade)
                {
                    _atividade = value;
                    
                    ///Mesmo nome utilizado no SelectItem
                    this.NotifyPropertyChanged("Atividade");
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
            get { return this._nomeTodo; }
            set
            {
                if (value != _nomeTodo)
                {
                    _nomeTodo = value;
                    this.NotifyPropertyChanged("NomeTodo");
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
            get { return _idTodo; }
            set
            {
                if (value != _idTodo)
                {
                    _idTodo = value;
                    this.NotifyPropertyChanged("IdTodo");
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
            get { return _completoTodo; }
            set
            {
                if (value != _completoTodo)
                {
                    _completoTodo = value;
                    this.NotifyPropertyChanged("CompletoTodo");
                }
            }
        }

        #endregion

        /// <summary>
        /// Métodos utilizados e acionados pelo click dos botões
        /// </summary>
        #region "#### Comandos ### "

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
        #endregion

        ///<summary>
        /// Inicia a View Model pelo Inicialize
        /// </summary>
        #region "##### Inicia a ViewModel "

        /// <summary>
        /// Método que inicia propriedades e comandos
        /// </summary>
        private void Initialize()
        {
            //Zera a lista
            this._listDeAtividadeToDo = null;
            this._listDeAtividadeToDo = new ObservableCollection<AtividadeToDo>();

            //Inicia os comandos
            this.SalvarAtividadeCommand = new SalvarAtividadeCommand(this);
            this.CarregarAtividadeCommand = new CarregarAtividadeCommand(this);
            this.DeletarAtividadeCommand = new DeletarAtividadeCommand(this);
            this.EditarAtividadeCommand = new EditarAtividadeCommand(this);
            this.CancelarEdicaoCommand = new CancelarEdicaoCommand(this);

            //carrega as atividades
            CarregarAtividadeCommand.Execute(null);
        }
        #endregion

        /// <summary>
        /// Construtor da classe
        /// </summary>
        #region " ### Construtor ##" 
        public ToDoViewModel()
        {
            //Chama o método que inicia
            this.Initialize();
        }
        #endregion
    }
}
