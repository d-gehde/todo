using System.Windows;
using System.Windows.Controls;
using todoList.ViewModel;

namespace todoList.Views
{
    /// <summary>
    /// Interaction logic for AddTeam.xaml
    /// </summary>
    public partial class AccountLogin : Window
    {
        public AccountLogin()
        {
            InitializeComponent();
            this.DataContext = Resources["loginViewModel"];
            //LoginViewModel lvm = new LoginViewModel(pwBox);
            ((LoginViewModel)DataContext).CloseAction = (Close);

        }

        private void pwBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Cast the 'sender' to a PasswordBox
              PasswordBox pBox = sender as PasswordBox;
 
              //Set this "EncryptedPassword" dependency property to the "SecurePassword"
              //of the PasswordBox.
            if (pBox != null) PasswordBoxMvvmAttachedProperties.SetEncryptedPassword(pBox, pBox.SecurePassword);
        }
    }
}
