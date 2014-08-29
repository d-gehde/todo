
using System.Collections.Generic;

namespace todoList.Model
{
    interface IUserModel
    {
        User CreateUser(string userName, string password, string prename = "", string surname = "", string eMail = "", string sId = "");

        User LoginUser(string name, string pw);

        bool ExistUserName(string name);

        bool UserDataChanged(User u);

        List<User> GetAllUsersInTeam(Team t);
        List<User> GetAllUsers();

        bool ChangeUserEMail(User u, string email);

        bool ChangeUserPrename(User u, string name);

        bool ChangeUserSurname(User u, string name);

        bool ChangeUserSkypeId(User u, string sId);

        bool RemoveUser(User u);
    }
}
