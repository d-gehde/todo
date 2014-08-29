using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace todoList.Views
{
    /// <summary>
    /// Interaction logic for EditTeam.xaml
    /// </summary>
    public partial class EditTeam : Window
    {

        public EditTeam()
        {
            this.lblHead.Content = "Add new Team:";
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        public EditTeam(Object obj)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.lblHead.Content = "Edit Team:";
        }
    }
}
