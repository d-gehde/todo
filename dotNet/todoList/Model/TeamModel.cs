using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace todoList.Model
{
    class TeamModel : ITeamModel
    {
        readonly ToDoModel e = new ToDoModel();

        public Team CreateTeam(List<User> u, string name)
        {
            Trace.WriteLine("Method CreateTeam in TeamModel entered.");
            var t = new Team(name);
            try
            {
                e.Teams.Add(t);
                
                e.SaveChanges();
                u.ForEach(x => e.UserTeamMatches.Add(new UserTeamMatch(x.Id, t.Id)));
                e.SaveChanges();
                
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Exception in Method CreateTeam:");
                Trace.WriteLine(ex, ex.Message);
                return null;
            }
            return t;
        }

        public Team LoadTeam(string name)
        {
            Trace.WriteLine("Method LoadTeam in TeamModel entered.");
            try
            {
                var list = (from t in e.Teams
                    where t.Name == name
                    select t).ToList();
                return list.Any() ? list.First() : null;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method LoadTeam:");
                Trace.WriteLine(ex, ex.Message);
                return null;
            }
        }

        public bool ChangeTeamName(Team t, string name)
        {
            Trace.WriteLine("Method ChangeTeamName in TeamModel entered.");
            try
            {
                t = LoadTeam(t.Name);
                t.Name = name;
                e.Entry(t).Property(x => x.Name).IsModified = true;
                e.SaveChanges();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method ChangeTeamName:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;

        }

        public bool AddUserToTeam(User u, Team t)
        {
            Trace.WriteLine("Method AddUserToTeam in TeamModel entered.");
            try
            {
                e.UserTeamMatches.Add(new UserTeamMatch(u.Id, t.Id));
                e.SaveChanges();
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Exception in Method AddUserToTeam:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool AddMultipleUsersToTeam(List<User> userList, Team t)
        {
            Trace.WriteLine("Method AddMultipleUsersToTeam in TeamModel entered.");
            return userList.All(u => AddUserToTeam(u, t));
        }

        public List<Team> GetAllTeams()
        {
            Trace.WriteLine("Method GetAllTeams in TeamModel entered.");
            try
            {
                return e.Teams.ToList();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method GetAllTeams:");
                Trace.WriteLine(ex, ex.Message);
                return null;
            }
            
        }

        public List<Team> GetAllTeamForUser(User u)
        {
            Trace.WriteLine("Method GetAllTeamForUser in TeamModel entered.");
            try
            {
                return (    from teams in e.Teams
                            join match in e.UserTeamMatches on u.Id equals match.UserID
                            where match.TeamID == teams.Id
                            select teams).ToList();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method GetAllTeamForUser:");
                Trace.WriteLine(ex, ex.Message);
                return null;
            }
        } 

        public bool RemoveUserFromTeam(User u, Team t)
        {
            Trace.WriteLine("Method RemoveUserFromTeam in TeamModel entered.");
            try
            {
                var userTeamMatch = e.UserTeamMatches.Find(new object[] {u.Id, t.Id});
                if (userTeamMatch != null)
                { 
                    e.UserTeamMatches.Remove(userTeamMatch);
                    e.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Exception in Method RemoveUserFromTeam:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool RemoveMultipleUsersFromTeam(List<User> userList, Team t)
        {
            Trace.WriteLine("Method RemoveMultipleUsersFromTeam in TeamModel entered.");
            return userList.All(u => RemoveUserFromTeam(u, t));
        }

        /// <summary>
        /// Removes a whole team
        /// Which means first it deletes all Teamtasks
        /// then the relation to those Tasks
        /// Eventually all the realtions to Users in the Team
        /// Finally the Team itself
        /// </summary>
        /// <param name="t">Team to delete</param>
        /// <returns>true if everything was Ok</returns>
        public bool RemoveTeam(Team t)
        {
            Trace.WriteLine("Method RemoveTeam in TeamModel entered.");
            try
            {
                t = LoadTeam(t.Name);

                //Delete all Team-Tasks
                var tasksToDelete = (   from tsk in e.Tasks
                                        join match in e.TeamTaskMatches on tsk.Id equals match.TaskID
                                        where match.TeamID == t.Id
                                        select tsk).AsEnumerable();
                e.Tasks.RemoveRange(tasksToDelete);
                

                //Delete every Relation to the deleted Tasks
                var taskRelationToDelete = (    from match in e.TeamTaskMatches
                                                where match.TeamID == t.Id
                                                select match).AsEnumerable();

                e.TeamTaskMatches.RemoveRange(taskRelationToDelete);

                
                //Delete every Relation to TeamUsers
                var userRelationToDelete = (from match in e.UserTeamMatches
                                            where match.TeamID == t.Id
                                            select match).AsEnumerable();

                e.UserTeamMatches.RemoveRange(userRelationToDelete);


                //Finally Delete the Team itself
                e.Teams.Remove(t);

                e.SaveChanges();
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Exception in Method RemoveTeam:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }

            return true;
        }
    }
}
