
using System.Windows.Input;

namespace todoList.ViewModel
{
    class TaskViewModel : BaseViewModel
    {
        private string title;
        private string priority;
        private string category;
        private string description;
        private string startdate;
        private string enddate;

        private ICommand addCmd;
        private ICommand editCmd;
        private Model.ITaskModel task;

        public TaskViewModel(Model.ITaskModel task)
        {
            this.task = task;
            addCmd = new DelegateCommand((param) => AddTask(), (param) => path.Length > 0);
            editCmd = new DelegateCommand((param) => EditTask(), (param) => path.Length > 0);
        }

        public ICommand AddCmd
        {
            get { return addCmd; }
        }

        public ICommand EditCmd
        {
            get { return editCmd; }
        }

        private void AddTask()
        {
            task.Add();
        }

        private void EditTask()
        {
            task.Edit();
        }
    }
}
