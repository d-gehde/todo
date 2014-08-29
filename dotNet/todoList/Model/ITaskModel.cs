using System;
using System.Collections.Generic;

namespace todoList.Model
{
    interface ITaskModel
    {
        Task CreateTaskForUser(User u, string title, DateTime dateCreated, DateTime dateBegin, DateTime dateEnd, int prio = 10, string description = "", string type = "TODO");
        Task CreateTaskForTeam(Team t, string title, DateTime dateCreated, DateTime dateBegin, DateTime dateEnd, int prio = 10, string description = "", string type = "TODO");

        List<Task> GetAllTasksForUser(User u);

        List<Task> GetAllTaksForTeam(Team t); 

        bool ChangeTaskBeginDate(Task t, DateTime dt);
        bool ChangeTaskEndDate(Task t, DateTime dt);
        bool ChangeTaskTitle(Task t, string title);
        bool ChangeTaskDescription(Task t, string description);
        bool ChangeTaskPrio(Task t, int prio);
        bool ChangeTaskType(Task t, string type);

        bool ChangeTask(Task t);

        bool RemoveTaskFromUser(Task t, User u);
        bool RemoveTaskFromTeam(Task tsk, Team t);

    }
}
