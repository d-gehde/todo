using System;
using System.Collections.Generic;

namespace todoList.Model
{
    interface ITeamModel
    {
        Team CreateTeam(List<User> u, string name);

        bool AddUserToTeam(User u, Team t);

        List<Team> GetAllTeams();

        List<Team> GetAllTeamForUser(User u);

        bool ChangeTeamName(Team t, string name);

        bool AddMultipleUsersToTeam(List<User> userList, Team t);

        bool RemoveUserFromTeam(User u, Team t);

        bool RemoveMultipleUsersFromTeam(List<User> userList, Team t);

        bool RemoveTeam(Team t);

        Team LoadTeam(string name);
    }
}
