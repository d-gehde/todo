using System;
using System.Windows;
using Google.GData.Client;
using Google.GData.Calendar;
using Google.GData.Extensions;

namespace GoogleApiSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

                When eventTimes = new When(new DateTime(2014, 08, 29), new DateTime(2014, 08, 30), true);
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
