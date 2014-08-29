using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todoList.Model
{
    interface IExport
    {
        bool ExportTask(Task t, string userMail, string pw);
        bool ExportMultipleTasks(List<Task> listTasks, string userMail, string pw);
    }
}
