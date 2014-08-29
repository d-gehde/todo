using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using Google.GData.Calendar;
using Google.GData.Client;
using Google.GData.Extensions;

namespace todoList.Model
{
    class GoogleCalendarExportModel : IExport
    {
        public bool ExportTask(Task t, string userMail, string pw)
        {
            Trace.WriteLine("Method ExportTask in GoogleCalendarExport entered.");
            try
            {
                
                Service service = new Service("cl", "TestName");

                service.setUserCredentials(userMail, pw);

                AtomPerson author = new AtomPerson(AtomPersonType.Author);
                author.Email = userMail;
                EventEntry entry = new EventEntry();
                entry.Authors.Add(author);
                entry.Title.Text = t.Title;
                entry.Content.Content = t.Description;

                When eventTimes = new When();
                eventTimes.StartTime = t.DateBegin;
                eventTimes.EndTime = t.DateEnd;
                entry.Times.Add(eventTimes);
                Uri postUri = new Uri("http://www.google.de/calendar/feeds/" + userMail + "/private/full");

                //not Used, does it matter?
                service.Insert(postUri, entry);
                
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in Method ExportTask:");
                Trace.WriteLine(ex, ex.Message);
                return false;
            }
            return true;
        }

        public bool ExportMultipleTasks(List<Task> listTasks, string userMail, string pw)
        {
            Trace.WriteLine("Method ExportMultipleTasks in GoogleCalendarExport entered.");
            listTasks.ForEach(x => ExportTask(x, userMail, pw));
            return true;
        }
    }
}
