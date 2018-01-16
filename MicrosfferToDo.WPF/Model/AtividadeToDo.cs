using System;
using System.ComponentModel;

namespace MicrosfferToDo.WPF.Model
{
    /// <summary>
    /// Classe responsável pela a atividade 
    /// <author>Mauricio Junior</author>
    /// </summary>
    public class AtividadeToDo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


       
        /// <summary>
        /// Id da Tabela de banco de dados
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
        /// Nome da atividade postada pelo usuário
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
        /// Propriedade informando o status 
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

    }
}
