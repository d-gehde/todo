using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using todoList.Model;

namespace todoList.ViewModel
{
    class AddTeamViewModel : BaseViewModel
    {
        private string name;
        private string lblTeamEditError;

        readonly private ICommand okCmd;
        readonly private ICommand cancelCmd;
        private ITeamModel teamModel;
        private IUserModel userModel;
        private List<UserInTeam> members = new List<UserInTeam>();

        private Team team;
        private User u;
        readonly private BindingList<Entity> tabs;

        public Action CloseAction { get; set; }

        private ObservableCollection<UserInTeam> isSelected = new ObservableCollection<UserInTeam>();
        private List<UserInTeam> wereSelected; 

        public AddTeamViewModel()
        {
            
        }

        public AddTeamViewModel(BindingList<Entity> tabs, User u) //model als parameter einfügen
        {
            this.tabs = tabs;
            this.u = u;
            Init();
            LoadUsers();

            okCmd = new DelegateCommand(param => AddTeam(), param => isSelected.Any());
            cancelCmd = new DelegateCommand(param => CancelNewTeam(), param => true);
        }
        
        public AddTeamViewModel(BindingList<Entity> tabs, Team t) //model als parameter einfügen
        {
            this.tabs = tabs;
            team = t;
            Init();
            LoadTeams();
            name = t.Name;
            okCmd = new DelegateCommand(param => EditTeam(), param => isSelected.Any());
            cancelCmd = new DelegateCommand(param => CancelNewTeam(), param => true);
        }

        public void Initialize()
        {
            wereSelected = isSelected.ToList();
        }

        private void Init()
        {
            userModel = new UserModel();
            teamModel = new TeamModel();
        }

        public ICommand OkCmd
        {
            get { return okCmd; }
        }

        public ICommand CancelCmd
        {
            get { return cancelCmd; }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                //NotifyPropertyChanged("Name");
            }
        }

        public string LblTeamEditError
        {
            get { return lblTeamEditError; }
            set
            {
                lblTeamEditError = value;
                NotifyPropertyChanged("LblTeamEditError");
            }
        }

        public ObservableCollection<UserInTeam> IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
            }
        }

        private void LoadUsers()
        {
            userModel.GetAllUsers().ForEach(x => Members.Add(new UserInTeam(x)));
        }

        private void LoadTeams()
        {
            userModel.GetAllUsers().ForEach(SetUserSelectedAsMember);
        }

        private void SetUserSelectedAsMember(User user)
        {
            UserInTeam t = new UserInTeam(user);
            List<User> l = userModel.GetAllUsersInTeam(team);
            if (l.Contains(user))
            {
                t.IsSelected = true;
            }
            Members.Add(t);
                
        }

        //private void SelectTeamMembers(Team t)
        //{
        //    userModel.GetAllUsersInTeam(t).ForEach(x => IsSelected.Add(new UserInTeam(x)));
        //}

        public List<UserInTeam> Members
        {
            get { return members; }
            private set
            {
                members = value;
                NotifyPropertyChanged("Members");
            }
        }

        private void AddTeam()
        {
            if (teamModel.LoadTeam(name) == null)
            {
                List<User> l = new List<User>();
                isSelected.ToList().ForEach(l.Add);
                var found = l.Any(uservar => uservar.Name == u.Name);
                if (found)
                {
                    tabs.Add(teamModel.CreateTeam(l, name));
                }
                else
                {
                    teamModel.CreateTeam(l, name);
                }
                
                CloseAction();
            }
            else
            {
                LblTeamEditError = "Team name already exists!";
            }
        }

        private void EditTeam()
        {
            if (teamModel.LoadTeam(name) == null || name ==  team.Name)
            {
                teamModel.ChangeTeamName(team, name);
                team.Name = name;
                NotifyPropertyChanged("TabTxt");
                tabs.ResetBindings();
                wereSelected.ForEach(RemoveUserFromTeam);
                IsSelected.ForEach(AddUserToTeam);
                CloseAction();
            }
                
            //teamModel.AddUserToTeam(IsSelected.First(), team);
            
        }

        private void RemoveUserFromTeam(UserInTeam user)
        {
            if (!IsSelected.Contains(user))
                teamModel.RemoveUserFromTeam(user, team);
        }

        private void AddUserToTeam(UserInTeam user)
        {
            if (!wereSelected.Contains(user))
                teamModel.AddUserToTeam(user, team);
        }

        private void CancelNewTeam()
        {
            // close window
            CloseAction();
        }

    }
}
