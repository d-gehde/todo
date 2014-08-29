
namespace todoList.Model
{
    sealed class UserInTeam : User
    {
        public bool IsSelected { get; set; }

        public UserInTeam()
        {
            IsSelected = false;
        }

        public UserInTeam(User u)
        {
            IsSelected = false;

            Id = u.Id;
            Name = u.Name;
            Password = u.Password;
            Surname = u.Surname;
            Prename = u.Prename;
            EMail = u.EMail;
            SkypeID = u.SkypeID;
            
        }

    }
}
