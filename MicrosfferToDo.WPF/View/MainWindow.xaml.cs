using MicrosfferToDo.WPF.ViewModel;
using System.Windows;

namespace MicrosfferToDo.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// <author>Mauricio Junior</author>
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //inicializa todo o contexto do aplicativo MVVM
            DataContext = new ToDoViewModel();
        }
    }
}
