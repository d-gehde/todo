using System.Diagnostics;
using System.Windows;

namespace todoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            TextWriterTraceListener tr1 = new TextWriterTraceListener(System.Console.Out);
            Trace.Listeners.Add(tr1);

            TextWriterTraceListener tr2 = new TextWriterTraceListener(System.IO.File.CreateText("ExceptionOutput.txt"));
            Trace.Listeners.Add(tr2);

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            
            // DataContext = new ViewModel.MainWindowViewModel();

        }
    }
}
