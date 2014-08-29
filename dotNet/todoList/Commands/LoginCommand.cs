using System;
using System.Windows.Input;
using todoList.ViewModel;

namespace todoList
{
    public class LoginCommand : ICommand
    {
        //Local instance of the ViewModel
        LoginViewModel loginViewModel = null;

        //Pass an instance of the ViewModel into the constructor
        public LoginCommand(LoginViewModel loginViewModel)
        {
            this.loginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            //Execution should only be possible if both Username and Password have been supplied
            if (!string.IsNullOrWhiteSpace(this.loginViewModel.Username) && this.loginViewModel.PasswordSecureString != null && this.loginViewModel.PasswordSecureString.Length > 0)
                return true;
            else
                return false;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.loginViewModel.Login();
        }
    }
}