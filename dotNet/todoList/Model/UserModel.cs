using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace todoList.Model
{
    class UserModel : IUserModel
    {
        readonly ToDoModel e = new ToDoModel();

        public User CreateUser(string userName, string password, string prename = "", string surname = "", string eMail = "", string sId = "")
        {
            Trace.WriteLine("Method CreateUser in UserModel entered.");
            var u = new User(userName, password, prename, surname, eMail, sId);
            try
            {
                e.Users.Add(u);
                e.SaveChanges();
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Exception in Method CreateUser:");
                Trace.WriteLine(ex, ex.Message);
                return null;
            }
            
            return u;
        }

        public User LoginUser(string name, string pw)
        {
            Trace.WriteLine("Method LoginUser in UserModel entered.");
            try
            {
                var result = e.Users.Where(x => (x.Name == name) && (x.Password == pw));
                if (!result.Any())
                {
                    return null;
                }
                return result.First();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method LoginUser:");
                Trace.WriteLine(ex, ex.Message);
                return null;
            }
        }

        public bool ExistUserName(string name)
        {
            Trace.WriteLine("Method ExistUserName in UserModel entered.");
            try
            {
                var userList = (from usr in e.Users
                    where usr.Name == name
                    select usr).ToList();

                if (userList.Any())
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method ExistUserName:");
                Trace.WriteLine(ex, ex.Message);
                return true;
            }
        }

        public List<User> GetAllUsersInTeam(Team t)
        {
            Trace.WriteLine("Method GetAllUsersInTeam in UserModel entered.");
            try
            {

                var usersInTeam = (from usr in e.Users
                    join match in e.UserTeamMatches on usr.Id equals match.UserID
                    where match.TeamID == t.Id
                    select usr).AsEnumerable();

                return usersInTeam.ToList();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Method GetAllUsers in UserModel entered.");
                return null;
            }
        } 

        public List<User> GetAllUsers()
        {
            Trace.WriteLine("Method GetAllUsers in UserModel entered.");
            return e.Users.ToList();
        }

        public bool UserDataChanged(User u)
        {
            Trace.WriteLine("Method UserDataChanged in UserModel entered.");
            try
            {
                e.Entry(u).Property(x => x.Name).IsModified = true;
                e.Entry(u).Property(x => x.Password).IsModified = true;
                e.Entry(u).Property(x => x.EMail).IsModified = true;
                e.Entry(u).Property(x => x.Surname).IsModified = true;
                e.Entry(u).Property(x => x.Surname).IsModified = true;
                e.Entry(u).Property(x => x.SkypeID).IsModified = true;
                e.SaveChanges();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method UserDataChanged:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool ChangeUserEMail(User u, string email)
        {
            Trace.WriteLine("Method ChangeUserEMail in UserModel entered.");
            try
            {
                u.EMail = email;
                e.Entry(u).Property(x => x.EMail).IsModified = true;
                e.SaveChanges();
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Exception in Method ChangeUserEMail:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool ChangeUserPrename(User u, string name)
        {
            Trace.WriteLine("Method ChangeUserPrename in UserModel entered.");
            try
            {
                u.Prename = name;
                e.Entry(u).Property(x => x.Prename).IsModified = true;
                e.SaveChanges();
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Exception in Method ChangeUserPrename:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool ChangeUserSurname(User u, string name)
        {
            Trace.WriteLine("Method ChangeUserSurname in UserModel entered.");
            try
            {
                u.Surname = name;
                e.Entry(u).Property(x => x.Surname).IsModified = true;
                e.SaveChanges();
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Exception in Method ChangeUserSurname:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool ChangeUserSkypeId(User u, string sId)
        {
            Trace.WriteLine("Method ChangeUserSkypeId in UserModel entered.");
            try
            {
                u.Prename = sId;
                e.Entry(u).Property(x => x.SkypeID).IsModified = true;
                e.SaveChanges();
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Exception in Method ChangeUserSkypeId:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool RemoveUser(User u)
        {
            Trace.WriteLine("Method RemoveUser in UserModel entered.");
            try
            {
                u = e.Users.Find(new object[] {u.Id});
                e.Users.Remove(u);
                e.SaveChanges();
            }
            catch(Exception ex)
            {
                Trace.WriteLine("Exception in Method RemoveUser:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }
    }
}
