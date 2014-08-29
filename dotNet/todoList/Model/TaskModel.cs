using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace todoList.Model
{
    class TaskModel : ITaskModel
    {
        readonly ToDoModel e = new ToDoModel();

        public Task CreateTaskForUser(User u, string title, DateTime dateCreated, DateTime dateBegin, DateTime dateEnd, int prio = 10,
            string description = "", string type = "TODO")
        {
            Trace.WriteLine("Method CreateTaskForUser in TaskModel entered.");
            Task t = new Task(title, description, dateCreated, dateBegin, dateEnd, type, prio);
            try
            {
                e.Tasks.Add(t);
                e.SaveChanges();
               
                e.UserTaskMatches.Add(new UserTaskMatch(u.Id, t.Id));
                e.SaveChanges();
                
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Exception in Method CreateTaskForUser:");
                Trace.WriteLine(ex, ex.Message);
                return null;
            }
            return t;
        }

        public Task CreateTaskForTeam(Team t, string title, DateTime dateCreated, DateTime dateBegin, DateTime dateEnd,
            int prio = 10, string description = "", string type = "TODO")
        {
            Trace.WriteLine("Method CreateTaskForTeam in TaskModel entered.");
            Task tsk = new Task(title, description, dateCreated, dateBegin, dateEnd, type, prio);
            try
            {
                //e.Tasks.Add(tsk);
                e.Tasks.Add(tsk);

                e.SaveChanges();

                e.TeamTaskMatches.Add(new TeamTaskMatch(t.Id, tsk.Id));

                e.SaveChanges();
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Exception in Method CreateTaskForTeam:");
                Trace.WriteLine(ex, ex.Message);
                return null;
            }
            return tsk;
        }

        public List<Task> GetAllTasksForUser(User u)
        {
            Trace.WriteLine("Method GetAllTasksForUser in TaskModel entered.");
            try
            {
                return (from t in e.Tasks
                    join ustskMatch in e.UserTaskMatches on t.Id equals ustskMatch.TaskID
                    where ustskMatch.UserID == u.Id
                    select t).ToList();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method GetAllTasksForUser:");
                Trace.WriteLine(ex, ex.Message);
                return null;
            }
            
        }

        public List<Task> GetAllTaksForTeam(Team t)
        {
            Trace.WriteLine("Method GetAllTaksForTeam in TaskModel entered.");
            try
            {
            return (    from tsk in e.Tasks
                        join teamtskMatch in e.TeamTaskMatches on tsk.Id equals teamtskMatch.TaskID
                        where teamtskMatch.TeamID == t.Id
                        select tsk).ToList();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method GetAllTaksForTeam:");
                Trace.WriteLine(ex, ex.Message);
                return null;
            }
        }

        public bool ChangeTaskBeginDate(Task t, DateTime dt)
        {
            Trace.WriteLine("Method ChangeTaskBeginDate in TaskModel entered.");
            try
            {
                t.DateBegin = dt;
                e.Entry(t).Property(x => x.DateBegin).IsModified = true;
                e.SaveChanges();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method ChangeTaskBeginDate:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool ChangeTaskEndDate(Task t, DateTime dt)
        {
            Trace.WriteLine("Method ChangeTaskEndDate in TaskModel entered.");
            try
            {
                t.DateEnd = dt;
                e.Entry(t).Property(x => x.DateEnd).IsModified = true;
                e.SaveChanges();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method ChangeTaskEndDate:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool ChangeTaskTitle(Task t, string title)
        {
            Trace.WriteLine("Method ChangeTaskTitle in TaskModel entered.");
            try
            {
                t.Title = title;
                e.Entry(t).Property(x => x.Title).IsModified = true;
                e.SaveChanges();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method ChangeTaskTitle:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool ChangeTaskDescription(Task t, string description)
        {
            Trace.WriteLine("Method ChangeTaskDescription in TaskModel entered.");
            try
            {
                t.Description = description;
                e.Entry(t).Property(x => x.Description).IsModified = true;
                e.SaveChanges();            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method ChangeTaskDescription:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool ChangeTaskPrio(Task t, int prio)
        {
            Trace.WriteLine("Method ChangeTaskPrio in TaskModel entered.");
            try
            {
                t.Priority = prio;
                e.Entry(t).Property(x => x.Priority).IsModified = true;
                e.SaveChanges();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method ChangeTaskPrio:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool ChangeTaskType(Task t, string type)
        {
            Trace.WriteLine("Method ChangeTaskType in TaskModel entered.");
            try
            {
                t.TaskType = type;
                e.Entry(t).Property(x => x.TaskType).IsModified = true;
                e.SaveChanges();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method ChangeTaskType:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool ChangeTask(Task t)
        {
            Trace.WriteLine("Method ChangeTask in TaskModel entered.");
            try
            {
                Task tt = e.Tasks.Find(new object[] {t.Id});

                tt.Title = t.Title;
                tt.Priority = t.Priority;
                tt.DateBegin = t.DateBegin;
                tt.DateEnd = t.DateEnd;
                tt.Description = t.Description;
                tt.TaskType = t.TaskType;


                e.Entry(tt).Property(x => x.Title).IsModified = true;
                e.Entry(tt).Property(x => x.Description).IsModified = true;
                e.Entry(tt).Property(x => x.Priority).IsModified = true;
                e.Entry(tt).Property(x => x.TaskType).IsModified = true;
                e.Entry(tt).Property(x => x.DateBegin).IsModified = true;
                e.Entry(tt).Property(x => x.DateEnd).IsModified = true;
                e.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method ChangeTask:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
        }

        public bool RemoveTaskFromUser(Task t, User u)
        {
            Trace.WriteLine("Method RemoveTaskFromUser in TaskModel entered.");
            try
            {
                t = e.Tasks.Find(new object[] {t.Id});
                e.Tasks.Remove(t);

                e.UserTaskMatches.Remove(e.UserTaskMatches.Find(new object[] {u.Id, t.Id}));
                e.SaveChanges();
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Exception in Method RemoveTaskFromUser:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool RemoveTaskFromTeam(Task tsk, Team t)
        {
            Trace.WriteLine("Method RemoveTaskFromTeam in TaskModel entered.");
            try
            {
                tsk = e.Tasks.Find(new object[] { tsk.Id });
                e.Tasks.Remove(tsk);

                e.TeamTaskMatches.Remove(e.TeamTaskMatches.Find(new object[] {t.Id, tsk.Id}));
                e.SaveChanges();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method RemoveTaskFromTeam:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

    }
}
