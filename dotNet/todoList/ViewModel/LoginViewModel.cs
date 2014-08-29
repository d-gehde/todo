using System;
using System.Security;
using System.Windows;
using System.Windows.Input;
using todoList.Model;
using todoList.Views;

namespace todoList.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string lblTeamEditError;

        readonly private LoginCommand loginCommand;
        readonly private ICommand cancelCmd;
        readonly private ICommand registerCmd;
        private IUserModel userModel;
        public Action CloseAction { get; set; }


        public LoginCommand UserLoginCommand
        {
            get { return loginCommand; }
        }


        private bool loginFailed;
        public bool LoginFailed
        {
            get { return loginFailed; }
            set
            {
                loginFailed = value;
                NotifyPropertyChanged("FailedLogin");
            }
        }

        //Property to hold the error message to be displayed
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if (value != errorMessage)
                {
                    errorMessage = value;
                    NotifyPropertyChanged("ErrorMessage");
                }
            }
        }

        //Properties bound to the specified username and password
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (!string.Equals(value, username, StringComparison.OrdinalIgnoreCase))
                {
                    username = value;
                    NotifyPropertyChanged("Username");
                }
            }
        }
        private SecureString password;
        public SecureString PasswordSecureString
        {
            get { return password; }
            set
            {
                if (value != null)
                {
                    password = value;
                    NotifyPropertyChanged("Password");
                }
            }
        }

        public LoginViewModel()
        {
            Init();
            loginCommand = new LoginCommand(this);
            cancelCmd = new DelegateCommand(param => CancelLogin(), param => true);
            registerCmd = new DelegateCommand(param => Register(), param => true);
        }

        private void Init()
        {
            userModel = new UserModel();
        }

        public ICommand CancelCmd
        {
            get { return cancelCmd; }
        }

        public ICommand RegisterCmd
        {
            get { return registerCmd; }
        }
        public string LblTeamEditError
        {
            get { return lblTeamEditError; }
            set
            {
                lblTeamEditError = value;
                NotifyPropertyChanged("LblTeamEditError");
            }
        }

        public void Login()
        {
            if (username != null)
            {
                var u = userModel.LoginUser(username, PasswordBoxMvvmAttachedProperties.ConvertToUnsecureString(password));
                
                if (u != null)
                {
                    var mwvm = new MainWindowViewModel(u);
                    
                    var mw = new MainWindow {DataContext = mwvm};
                    mw.Show();
                    
                    CloseAction();
                }
                else
                {
                    LblTeamEditError = "User or Password wrong!";
                    username = "";
                    password.Clear();
                }
            }
        }

        private void CancelLogin()
        {
            Application.Current.Shutdown(0);
        }

        private void Register()
        {
            var euvm = new EditUserViewModel();
            var siv = new EditUser {DataContext = euvm};

            // Bind the view to the viewmodel  
            euvm.CloseAction = (siv.Close);
            // Show the view modeless  
            siv.Show();
            CloseAction();
        }

    }
}