using System;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Input;
using todoList.Model;

namespace todoList.ViewModel
{
    class GoogleExportViewModel : BaseViewModel
    {

        readonly private ICommand exportCmd;
        readonly private ICommand abortCmd;

        readonly IExport e = new GoogleCalendarExportModel();

        private string email = "";

        readonly private BindingList<Task> tasks = new BindingList<Task>();

        public Action CloseAction { get; set; }

        public GoogleExportViewModel()
        {
            
        }

        public GoogleExportViewModel(BindingList<Task> tasks)
        {
            this.tasks = tasks;
            exportCmd = new DelegateCommand(param => GoogleExport(), param => email.Contains("@"));
            abortCmd = new DelegateCommand(param => Abort(), param => true);
        }

        private void GoogleExport()
        {
            e.ExportMultipleTasks(tasks.ToList(), email, PasswordBoxMvvmAttachedProperties.ConvertToUnsecureString(password));
            CloseAction();
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

        private void Abort()
        {
            CloseAction();
        }

        public ICommand ExportCmd
        {
            get { return exportCmd; }
        }

        public ICommand AbortCmd
        {
            get { return abortCmd; }
        }

        public string EMail
        {
            get { return email; }
            set { email = value; }
        }

    }
}
