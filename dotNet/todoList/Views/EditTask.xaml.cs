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
    /// Interaction logic for EditTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        public EditTask()
        {
            //this.LblHead.Content = "Add Task:";
            // this.btnOk.Click =;
            InitializeComponent();
        }

        public EditTask(Object obj)
        {
            //this.LblHead.Content = "Edit Task:";
            //this.TxtbTitle.Text = "Title";
            this.TxtbCategory.Text = "Category";
            this.TxtbPriority.Text = "Prio";
            this.TxtbStartDate.Text = "Start";
            this.TxtbEndDate.Text = "End";
            this.TxtbDescription.Text = "Description";

            InitializeComponent();
        }
    }
}
