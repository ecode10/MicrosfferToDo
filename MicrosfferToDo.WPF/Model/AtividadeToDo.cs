using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosfferToDo.WPF.Model
{
    public class AtividadeToDo : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region "##### Propriedades"
        private string _nomeTodo;

        public string NomeTodo
        {
            get { return _nomeTodo; }
            set
            {
                _nomeTodo = value;
                this.NotifyPropertyChanged("NomeTodo");
            }
        }

        //private Int64 _idTodo;
        //public Int64 IdTodo
        //{
        //    get { return _idTodo; }
        //    set
        //    {
        //        _idTodo = value;
        //        //this.NotifyPropertyChanged("IdTodo");
        //    }
        //}

        //private int _completoTodo;
        //public int CompletoTodo
        //{
        //    get { return _completoTodo; }
        //    set
        //    {
        //        _completoTodo = value;
        //        //this.NotifyPropertyChanged("CompletoTodo");
        //    }
        //}

        #endregion
    }
}
