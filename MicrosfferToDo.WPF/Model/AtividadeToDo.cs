using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosfferToDo.WPF.Model
{
    /// <summary>
    /// Classe responsável pela a atividade 
    /// <author>Mauricio Junior</author>
    /// </summary>
    public class AtividadeToDo : INotifyPropertyChanged
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
        /// Construtor da classe
        /// </summary>
        #region "### Construtor "
        public AtividadeToDo() { }
        #endregion

        /// <summary>
        /// Propriedades utilizada da classe e da UI
        /// </summary>
        #region "#### Propriedades "

        /// <summary>
        /// Id da Tabela de banco de dados
        /// </summary>
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

        /// <summary>
        /// Nome da atividade postada pelo usuário
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
        /// Propriedade informando o status 
        /// Completou 1 
        /// Não completou 0
        /// </summary>
        private int _completoTodo;
        public int CompletoTodo
        {
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
        #endregion
    }
}
