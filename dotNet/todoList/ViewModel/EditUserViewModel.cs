using System;
using System.Windows;
using System.Windows.Input;
using todoList.Model;

namespace todoList.ViewModel
{
    class EditUserViewModel
    {
        private string name = "", password = "", prename, surname, email, skypeId;
        readonly private ICommand okCmd;
        readonly private ICommand cancelCmd;
        private IUserModel userModel;
        private User u;


        public Action CloseAction { get; set; }

        public EditUserViewModel()
        {
            Init();
            Header = "Register New User";
            okCmd = new DelegateCommand(param => RegisterUser(), param => name != ""  && password != "");
            cancelCmd = new DelegateCommand(param => CancelRegister(), param => true);
        }

        public EditUserViewModel(User u)
        {
            this.u = u;
            Init();
            Header = "Edit User";
            LoadUserdata();
            okCmd = new DelegateCommand(param => EditUser(), param => true);
            cancelCmd = new DelegateCommand(param => CancelRegister(), param => true);
        }

        private void Init()
        {
            userModel = new UserModel();
        }

        private void LoadUserdata()
        {
            Name = u.Name;
            Password = null;
            Prename = u.Prename;
            Surname = u.Surname;
            email = u.EMail;
            skypeId = u.SkypeID;
        }

        public ICommand OkCmd
        {
            get { return okCmd; }
        }

        public ICommand CancelCmd
        {
            get { return cancelCmd; }
        }

        public string Header { get; private set; }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                //NotifyPropertyChanged("name");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                //NotifyPropertyChanged("password");
            }
        }


        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                //NotifyPropertyChanged("StartDate");
            }
        }

        public string SkypeId
        {
            get { return skypeId; }
            set
            {
                skypeId = value;
                //NotifyPropertyChanged("EndDate");
            }
        }


        public string Prename
        {
            get { return prename; }
            set
            {
                prename = value;
                //NotifyPropertyChanged("prename");
            }
        }
        private void RegisterUser()
        {
            if (!userModel.ExistUserName(Name))
            {
                u = userModel.CreateUser(Name, Password, Prename, Surname, Email, SkypeId);
                if (u != null)
                {
                    MainWindowViewModel mwvm = new MainWindowViewModel(u);

                    MainWindow mw = new MainWindow();
                    mw.DataContext = mwvm;
                    mw.Show();

                    CloseAction();
                }
            }
            

        }
        private void EditUser()
        {
            u.Name = Name;
            if (password != null)
                u.Password = password;
            u.Prename = Prename;
            u.Surname = Surname;
            u.EMail = Email;
            u.SkypeID = SkypeId;

            if (userModel.UserDataChanged(u))
                CloseAction();

        }
        private void CancelRegister()
        {
            Application.Current.Shutdown(0);
        }

    }
}
