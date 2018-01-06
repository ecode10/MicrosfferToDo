using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosfferToDo.WPF
{
    public class AtividadesToDo : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private Int64 _idTodo;
        public Int64 IdTodo
        {
            get { return this._idTodo; }
            set
            {
                if (value != _idTodo)
                {
                    _idTodo = value;
                    this.NotifyPropertyChanged("IdTodo");
                }
            }
        }

        private string _nomeTodo;
        public string NomeTodo
        {
            get { return this._nomeTodo; }
            set {
                if (value != _nomeTodo)
                {
                    _nomeTodo = value;
                    this.NotifyPropertyChanged("NomeTodo");
                }
            }
        }

        private int _completoTodo;
        public int CompletoTodo {
            get { return this._completoTodo; }
            set
            {
                if (value != _completoTodo)
                {
                    _completoTodo = value;
                    this.NotifyPropertyChanged("CompletoTodo");
                }
            }
        }
    }
}
