using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace todoList.Model
{
    public static class EmailNotification
    {
        private const string CancelProjectSubjectBody = "?Subject=Project X at the Date of Z got cancelled?";

        private const string StandardMailSubjectBody = "?Subject=Predefine Subject...";

        public static void GenerateStandardEmailToAllMember(List<User> memberList)
        {
            Trace.WriteLine("Method GenerateStandardEmailToAllMember in EmailNotifications entered.");
           string emailString = memberList.Where(member => member.EMail.Contains("@")).Aggregate("Mailto:", (current, member) => current + (member.EMail + ";"));
            emailString += CancelProjectSubjectBody;
            Process.Start(emailString);
        }


         public static void GenerateStandardEmailToAMember(User member)
        {
            Trace.WriteLine("Method GenerateStandardEmailToAMember in EmailNotifications entered.");
             if (member.EMail.Contains("@"))
             {
                 string emailString = "Mailto:";

                 emailString += member.EMail + StandardMailSubjectBody;
                 try
                 {
                     Process.Start(emailString);
                 }
                 catch (System.ComponentModel.Win32Exception noEmail)
                 {
                     if (noEmail.ErrorCode == -2147467259)
                     {
                         Trace.WriteLine("Exception in Method GenerateStandardEmailToAMember:");
                         Trace.WriteLine("System.ComponentModel.Win32Exception noEmail, Description: No Browser found!");
                     }
                 }
                 catch (Exception other)
                 {
                     Trace.WriteLine("Exception in Method GenerateStandardEmailToAMember:");
                     Trace.WriteLine(other, other.Message);
                 }
             }
        }

    }
}
