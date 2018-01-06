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
    public class ToDoViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region "### Propriedades "

        //propriedades utilizadas na view
        private ObservableCollection<AtividadeToDo> _listDeAtividadeToDo;

        public ObservableCollection<AtividadeToDo> ListDeAtividadeToDo
        {
            get { return _listDeAtividadeToDo; }

            set
            {
                _listDeAtividadeToDo = value;
                this.NotifyPropertyChanged("ListDeAtividadeToDo");

            }
        }

        private AtividadeToDo _atividade;

        public AtividadeToDo Atividade
        {
            get { return _atividade; }
            set
            {

                if (value != _atividade)
                {
                    _atividade = value;
                    //Notificar alteração da propriedade
                    this.NotifyPropertyChanged("Atividade");
                }
            }
        }

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

        #region "#### Comandos ### "
        public ICommand SalvarAtividadeCommand { get; set; }

        public ICommand DeletarAtividadeCommand { get; set; }

        public ICommand CarregarAtividadeCommand { get; set; }

        //public ICommand AtualizarAtividadeCommand { get; set; }
        #endregion

        #region "##### Inicia a ViewModel "

        private void Initialize()
        {
            this._listDeAtividadeToDo = null;
            this._listDeAtividadeToDo = new ObservableCollection<AtividadeToDo>();

            this.SalvarAtividadeCommand = new SalvarAtividadeCommand(this);
            this.CarregarAtividadeCommand = new CarregarAtividadeCommand(this);
            this.DeletarAtividadeCommand = new DeletarAtividadeCommand(this);

            CarregarAtividadeCommand.Execute(null);

            
        }
        #endregion

        #region " ### Construtor ##" 
        public ToDoViewModel()
        {
            this.Initialize();
        }
        #endregion
    }
}
