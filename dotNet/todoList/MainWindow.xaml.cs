using System.Windows;
using System.Windows.Media.Media3D;

namespace todoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.TaskViewModel(new Model.TaskModel());
        }
    }
}
