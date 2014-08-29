using System;
using Google.GData.Calendar;
using Google.GData.Client;
using Google.GData.Extensions;

namespace GoogleApiSample
{
    internal class User
    {
        private string name;
        private string email;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public User()
        {
            init();
        }

        private void init()
        {
            try
            {
                Service service = new Service("Test");
                service.setUserCredentials("denis.gehde@gmail.com", "");

                AtomPerson person = new AtomPerson(AtomPersonType.Author);
                person.Email = "denis.gehde@gmail.com";
                EventEntry entry = new EventEntry();
                entry.Authors.Add(person);
                entry.Title.Text = "Dies ist ein Test";
                entry.Content.Content = "lalalalalla";

                When eventTimes = new When(new DateTime(2014,08,29),new DateTime(2014,08,30),true);
                entry.Times.Add(eventTimes);
                Uri post = new Uri("http://www.google.de/calendar/feeds/" + "denis.gehde@gmail.com" + "/private/full");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
