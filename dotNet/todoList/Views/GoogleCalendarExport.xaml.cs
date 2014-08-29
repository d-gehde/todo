using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using todoList.ViewModel;

namespace todoList.Views
{
    /// <summary>
    /// Interaction logic for GoogleCalendarExport.xaml
    /// </summary>
    public partial class GoogleCalendarExport : Window
    {
        public GoogleCalendarExport()
        {
            InitializeComponent();
        }

        private void PwBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Cast the 'sender' to a PasswordBox
            var pBox = sender as PasswordBox;

            //Set this "EncryptedPassword" dependency property to the "SecurePassword"
            //of the PasswordBox.
            if (pBox != null) PasswordBoxMvvmAttachedProperties.SetEncryptedPassword(pBox, pBox.SecurePassword);
        }
    }
}
