using System;
using System.ComponentModel;
using System.Windows.Input;
using todoList.Model;

namespace todoList.ViewModel
{
    class AddTaskViewModel
    {
        private string titel ="", category = "", description, header;
        private int priority;
        private DateTime startDate, endDate;
        readonly private ICommand okCmd;
        private ICommand cancelCmd;
        private Task task;
        private ITaskModel taskModel;
        private IUserModel userModel = new UserModel();
        
        public Action CloseAction { get; set; }

        private Entity taskEntity;
        private BindingList<Task> tasks;

        public AddTaskViewModel()
        {

        }

        public AddTaskViewModel(BindingList<Task> tasks, Entity t)
        {
            MailBoxCheckedVisible = false;
            this.tasks = tasks;
            taskEntity = t;
            Init();
            header = "Add Task";
            okCmd = new DelegateCommand(param => AddTask(), param => titel != "" && category != "");
            cancelCmd = new DelegateCommand(param => CancelNewTask(), param => true);

            startDate = DateTime.Now;
            endDate = DateTime.Now;
           
        }

        public AddTaskViewModel(BindingList<Task> tasks, Task task) //model als parameter einfügen
        {
            MailBoxCheckedVisible = false;
            this.tasks = tasks;
            Init();
            header = "Edit Task";
            this.task = task;

            titel = task.Title;
            category = task.TaskType;
            description = task.Description;
            priority = (int)task.Priority;
            startDate = task.DateBegin;
            endDate = task.DateEnd;

            okCmd = new DelegateCommand(param => EditTask(), param => true);
            cancelCmd = new DelegateCommand(param => CancelNewTask(), param => true);
        }

        private void Init()
        {
            taskModel = new TaskModel();
        }

        public ICommand OkCmd
        {
            get { return okCmd; }
        }

        public ICommand CancelCmd
        {
            get { return cancelCmd; }
        }

        public string Header
        {
            get { return header; }
        }

        public string Titel
        {
            get { return titel; }
            set
            {
                titel = value;
                //NotifyPropertyChanged("Titel");
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                //NotifyPropertyChanged("Category");
            }
        }


        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                //NotifyPropertyChanged("StartDate");
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                //NotifyPropertyChanged("EndDate");
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                //NotifyPropertyChanged("Description");
            }
        }

        public int Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                //NotifyPropertyChanged("Priority");
            }
        }

        private void AddTask()
        {
            
            if(taskEntity is User)
                tasks.Add(taskModel.CreateTaskForUser((User)taskEntity, titel, DateTime.Now, startDate, endDate, priority, description, category));
            else
            {
                if (MailBoxChecked)
                {
                    EmailNotification.GenerateStandardEmailToAllMember(userModel.GetAllUsersInTeam((Team)taskEntity));
                }
                tasks.Add(taskModel.CreateTaskForTeam((Team)taskEntity, titel, DateTime.Now, startDate, endDate, priority, description, category));
            }
            CloseAction();
        }

        private void EditTask()
        {
            task.Title = titel;
            task.Description = description;
            task.Priority = priority;
            task.TaskType = category;
            task.DateBegin = StartDate;
            task.DateEnd = EndDate;

            taskModel.ChangeTask(task);

            tasks.ResetBindings();
            CloseAction();
        }

        private void CancelNewTask()
        {
            // close window
            CloseAction();
        }

        public bool MailBoxChecked { get; set; }

        public bool MailBoxCheckedVisible { get; set; }
    }
}
