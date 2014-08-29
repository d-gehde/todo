using System;
using System.Windows.Input;

namespace todoList.ViewModel
{
    class TaskDetailsViewModel : BaseViewModel
    {
        private readonly ICommand okCmd;

        readonly private string title, category, description;
        private readonly int priority;
        readonly private DateTime dateCreated, dateBegin, dateEnd;

        public Action CloseAction { get; set; }

        public TaskDetailsViewModel()
        {
            
        }

        public TaskDetailsViewModel(Task t)
        {
            title = t.Title;
            category = t.TaskType;
            if (t.Priority != null) priority = (int) t.Priority;
            description = t.Description;
            if (t.DateCreated != null) dateCreated = (DateTime) t.DateCreated;
            dateBegin = t.DateBegin;
            dateEnd = t.DateEnd;

            okCmd = new DelegateCommand(param => CloseDetailsWindow(), param => true);
        }

        private void CloseDetailsWindow()
        {
            CloseAction();
        }

        public string Title
        {
            get { return title; }
        }

        public string Category
        {
            get { return category; }
        }

        public int Priority
        {
            get { return priority; }
        }

        public DateTime DateCreated
        {
            get { return dateCreated; }
        }

        public DateTime DateBegin
        {
            get { return dateBegin; }
        }

        public DateTime DateEnd
        {
            get { return dateEnd; }
        }

        public string Description
        {
            get { return description; }
        }

        public ICommand OkCmd
        {
            get { return okCmd; }
        }
    }
}
