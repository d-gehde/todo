using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using todoList.Model;
using todoList.Views;

namespace todoList.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        readonly private ITaskModel taskModel = new TaskModel();
        //readonly private IUserModel userModel = new UserModel();
        readonly private ITeamModel teamModel = new TeamModel();

        readonly private ICommand addCmd;
        readonly private ICommand editCmd;
        readonly private ICommand deleteCmd;
        readonly private ICommand addCmdTeam;
        readonly private ICommand editCmdTeam;
        readonly private ICommand deleteCmdTeam;
        readonly private ICommand exitCmd;
        readonly private ICommand googleExportCmd;
        private readonly ICommand doublClickTaskDetailsCmd;

        readonly private User u;

        private Task isSelected;
        readonly private BindingList<Task> tasks = new BindingList<Task>();
        private Entity tabTxt;
        private readonly BindingList<Entity> tabs = new BindingList<Entity>();

        public Action CloseAction { get; set; }

        public MainWindowViewModel()
        {
            
        }

        public MainWindowViewModel(User u)
        {
            this.u = u;
            Init();

            exitCmd = new DelegateCommand(param => Exit(), param => true);
            googleExportCmd = new DelegateCommand(param => GoogleExport(), param => tasks.Any());

            addCmd = new DelegateCommand(param => AddTask(), param => true);
            //editCmd = new DelegateCommand(param => EditTask(), param => isSelected.Any());
            //deleteCmd= new DelegateCommand(param => DeleteTask(), param => isSelected.Any());

            editCmd = new DelegateCommand(param => EditTask(), param => isSelected != null);
            deleteCmd= new DelegateCommand(param => DeleteTask(), param => isSelected != null);

            addCmdTeam = new DelegateCommand(param => AddTeam(), param => true);
            editCmdTeam = new DelegateCommand(param => EditTeam(), param => tabTxt is Team);
            deleteCmdTeam = new DelegateCommand(param => DeleteTeam(), param => tabTxt is Team);

            doublClickTaskDetailsCmd = new DelegateCommand(param => DetailsTask(), param => true);
        }

        private void Init()
        {
            tabs.Add(u);
            tabTxt = u;
            teamModel.GetAllTeamForUser(u).ForEach(tabs.Add);
            taskModel.GetAllTasksForUser(u).ForEach(tasks.Add);
        }

        private void ChangeTab()
        {
            if (tabTxt == null)
                tabTxt = tabs.First();
            tasks.Clear();
            if (tabTxt is Team)
            {
                taskModel.GetAllTaksForTeam((Team)tabTxt).ForEach(tasks.Add);
            }
            else
            {
                taskModel.GetAllTasksForUser((User)tabTxt).ForEach(tasks.Add);
            }
        }

        private void AddTask()
        {

            var sivm = new AddTaskViewModel(tasks, tabTxt);
            var siv = new AddTask {DataContext = sivm};

            // Bind the view to the viewmodel  

            if (sivm.CloseAction == null)
                sivm.CloseAction = (siv.Close);

            // Show the view modeless  
            siv.Show();
        }

        private void EditTask()
        {
            
           // Tasks.Add(new Task("peter", "anders", DateTime.Now, DateTime.Now, DateTime.Now, "TODO", 5));
            //AddTaskViewModel sivm = new AddTaskViewModel(Tasks, isSelected.ToList()[0]);
            var sivm = new AddTaskViewModel(Tasks, isSelected);
            var siv = new AddTask {DataContext = sivm};
  
            // Bind the view to the viewmodel  

            if (sivm.CloseAction == null)
                sivm.CloseAction = (siv.Close);

            // Show the view modeless  
            siv.Show();
             
        }

        //TODO IDialog statt MessageBox
        private void DeleteTask()
        {

            // Configure the message box to be displayed
            const string messageBoxText = "Delete task irreplacable?";
            const string caption = "Delete Task";

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Warning);

            // Process message box results
            if (result == MessageBoxResult.Yes)
            {
                //var t = isSelected.ToList()[0];
                var t = isSelected;
                bool removed;
                if (tabTxt is User)
                {
                    removed = taskModel.RemoveTaskFromUser(t, u);   
                }
                else
                {
                    removed = taskModel.RemoveTaskFromTeam(t, (Team) tabTxt);
                }
                if (removed)
                {
                    tasks.Remove(t);
                    isSelected = null;
                }
            }
   
        }

        private void AddTeam()
        {
            var sivm = new AddTeamViewModel(tabs, u);
            var siv = new AddTeam {DataContext = sivm};

            // Bind the view to the viewmodel  


            if (sivm.CloseAction == null)
                sivm.CloseAction = (siv.Close);

            // Show the view modeless  
            siv.Show();
        }

        private void EditTeam()
        {
            var sivm = new AddTeamViewModel(tabs, (Team)tabTxt);
            
            var siv = new AddTeam {DataContext = sivm, LblHeader = {Content = "Edit Team"}, Title = "Edit Team"};
            //sivm.Initialize();
            // Bind the view to the viewmodel  

            if (sivm.CloseAction == null)
                sivm.CloseAction = (siv.Close);

            // Show the view modeless  
            siv.Show();
            sivm.Initialize();
        }

        private void DeleteTeam()
        {
            if (teamModel.RemoveTeam((Team) tabTxt))
            {
                tabs.Remove(tabTxt);
                tabTxt = tabs.First();
                NotifyPropertyChanged("TabTxt");
                ChangeTab();
            }
        }

        private void DetailsTask()
        {
            var sivm = new TaskDetailsViewModel(isSelected);

            var siv = new TaskDetail {DataContext = sivm};
            //sivm.Initialize();
            // Bind the view to the viewmodel  

            if (sivm.CloseAction == null)
                sivm.CloseAction = (siv.Close);

            // Show the view modeless  
            siv.Show();
        }
        
        //public ObservableCollection<Task> IsSelected
        //{
        //    get { return isSelected; }
        //    set
        //    {
        //        isSelected = value;
        //    }
        //}
        
        public Task IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
            }
        }

        public BindingList<Task> Tasks
        {
            get { return tasks; }
        }

        public Entity TabTxt
        {
            get { return tabTxt; }
            set
            {
                tabTxt = value;
                ChangeTab();
            }
        }

        public BindingList<Entity> Tabs
        {
            get { return tabs; }
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        private void GoogleExport()
        {
            var gevm = new GoogleExportViewModel(tasks);

            var siv = new GoogleCalendarExport { DataContext = gevm};

            if (gevm.CloseAction == null)
                gevm.CloseAction = (siv.Close);

            siv.Show();
        }

        #region Commands

        public ICommand EditCmd
        {
            get { return editCmd; }
        }

        public ICommand AddCmd
        {
            get { return addCmd; }
        }

        public ICommand DelCmd
        {
            get { return deleteCmd; }
        }

        public ICommand AddCmdTeam
        {
            get { return addCmdTeam; }
        }

        public ICommand EditCmdTeam
        {
            get { return editCmdTeam; }
        }

        public ICommand DeleteCmdTeam
        {
            get { return deleteCmdTeam; }
        }

        public ICommand ExitCmd
        {
            get { return exitCmd; }
        }

        public ICommand GoogleExportCmd
        {
            get { return googleExportCmd; }
        }

        public ICommand DoublClicktTaskDetailsCmd
        {
            get { return doublClickTaskDetailsCmd; }
        }

 #endregion
    }
}
